using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreRCON.Parsers.OHD
{
    public enum PlayerType
    {
        Human,
        Bot,
        Spectator
    }

    public class Status_Player
    {
        public PlayerType Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong NetworkId { get; set; }
        public string NetworkIdString { get; set; }
    }

    public class Status : IParseable
    {
        public string ServerName { get; set; }
        public string Map { get; set; }
        public string GameMode { get; set; }
        public string MatchState { get; set; }
        public string SessionState { get; set; }
        public bool Hibernating { get; set; }

        public int Humans { get; set; }
        public int Bots { get; set; }
        public int Spectators { get; set; }
        public int MaxPlayers { get; set; }

        public string LocalHost { get; set; }
        public string PublicHost { get; set; }

        public List<Status_Player> PlayerList { get; set; }
    }

    public class StatusParser : IParser<Status>
    {
        public string Pattern => throw new System.NotImplementedException();

        public bool IsMatch(string input)
        {
            return input.Contains("Server Name: ") | input.Contains("Is In Hibernation Mode:");
        }

        public Status Load(GroupCollection groups)
        {
            throw new System.NotImplementedException();
        }

        public Status Parse(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var groups = lines
                .Select(x => x.Split(':'))
                .Where(x => x.Length > 1 && !string.IsNullOrEmpty(x[0].Trim()) && !string.IsNullOrEmpty(x[1].Trim()))
                .ToDictionary(x => x[0].Trim(), x => string.Join(":", x.ToList().Skip(1)).Trim());

            string hostname = null;
            groups.TryGetValue("Server Name", out hostname);
            string localIp = null, publicIp = null;
            groups.TryGetValue("Local Address", out localIp);

            string map = null;
            groups.TryGetValue("Map", out map);
            string gameMode = null;
            groups.TryGetValue("Game Mode", out gameMode);

            bool hibernating = false;
            if (groups.TryGetValue("Is In Hibernation Mode", out string hibernation))
            {
                hibernating = string.Compare(hibernation, "Yes", true) == 0;
            }

            string matchState = null;
            groups.TryGetValue("Match State", out matchState);
            string sessionState = null;
            groups.TryGetValue("Session State", out sessionState);

            int players = 0, bots = 0, spectators = 0, maxPlayers = 0;
            var playerString = lines.FirstOrDefault(x => x.Contains("Human Players") && x.Contains("Bots") && x.Contains("Spectators"));
            if (playerString != null)
            {
                var match = Regex.Match(playerString, "(\\d+) Human Players, (\\d+) Bots, (\\d+) Spectators.*");
                if (match.Success)
                {
                    players = int.Parse(match.Groups[1].Value);
                    bots = int.Parse(match.Groups[2].Value);
                    spectators = int.Parse(match.Groups[3].Value);
                }
            }

            var playersList = new List<Status_Player>();

            var extractPlayers = false;
            foreach (var line in lines)
            {
                if (!extractPlayers)
                {
                    // find start of list
                    if (line.Contains("Id #") && line.Contains("Player Name") && line.Contains("Unique Net Id"))
                    {
                        extractPlayers = true;
                    }
                }
                else
                {
                    var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3)
                    {
                        var type = PlayerType.Bot;
                        var id = int.Parse(parts.First());
                        var networkIdString = string.Empty;
                        if (ulong.TryParse(parts.Last(), out ulong networkId))
                        {
                            type = PlayerType.Human;
                            networkIdString = parts.Last();
                        }
                        var name = string.Join(" ", parts, 1, parts.Length - 2);
                        playersList.Add(new Status_Player() { Type = type, Id = id, Name = name, NetworkId = networkId, NetworkIdString = networkIdString });
                    }

                    // find end of list
                    if (line.Contains("="))
                    {
                        break;
                    }
                }
            }

            return new Status()
            {
                ServerName = hostname,
                Map = map,
                GameMode = gameMode,
                MatchState = matchState,
                SessionState = sessionState,
                Hibernating = hibernating,
                Humans = players,
                Bots = bots,
                Spectators = spectators,
                MaxPlayers = maxPlayers,
                LocalHost = localIp,
                PublicHost = publicIp,
                PlayerList = playersList
            };
        }

        public Status Parse(Group group)
        {
            throw new System.NotImplementedException();
        }
    }
}
