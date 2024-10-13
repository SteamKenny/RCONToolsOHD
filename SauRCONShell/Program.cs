using CoreRCON;
using System.Net;
using System.Text.RegularExpressions;

namespace SauRCONShell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("SauRCONShell <password>@<host>:<port>");
                return;
            }

            var connection = args[0];

            var r = new Regex(@"^(?<password>\w+)@(?<host>.+):(?<port>\d+)", RegexOptions.None);
            var  m = r.Match(connection);

            if (!m.Success)
            {
                Console.WriteLine($"Error: Could not parse connection provided - {connection}?");
                return;
            }

            Console.Title = "SauRCON Shell";
            Console.WriteLine("SauRCON Shell for OHD");

            var host = m.Groups["host"].Value;
            var port = int.Parse(m.Groups["port"].Value);
            var password = m.Groups["password"].Value;

            // connect to the server
            var addresses = Dns.GetHostAddresses(host);
            var ip = addresses.First(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            var ipep = new IPEndPoint(ip, port);
            var rcon = new RCON(ipep, password, autoConnect: false, dynamicMultiPacketSupport: true);
            rcon.OnDisconnected += () =>
            {
                rcon.Dispose();
                Console.WriteLine("Disconnected!");
            };
            Console.WriteLine($"Connecting to {ipep} ...");
            try
            {
                rcon.ConnectAsync().Wait();
                if (rcon.Connected && rcon.Authenticated)
                {
                    Loop(rcon);

                    rcon.Dispose();
                    Console.WriteLine("Disconnected!");
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine($"EXCEPTION: {e.Message}");
            }
        }

        static void Loop(RCON connection)
        {
            Console.WriteLine("Connected!");

            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

            while (true)
            {
                var command = ReadLine.Read("(RCON)> ");

                if (command != null)
                {
                    command = command.Trim();

                    if (command == "exit" || command == "quit")
                    {
                        break;
                    }
                    else if (command == "EXIT" || command == "QUIT")
                    {
                        // prevent exit/quit from being called on the server
                        Console.WriteLine("Are you really sure you wanted to stop the server?");
                        Console.WriteLine("If so, then use the command 'shutdown'.");
                    }
                    else if (command == "help")
                    {
                        var commands = new AutoCompletionHandler().autos;
                        foreach (var cmd in commands)
                        {
                            Console.WriteLine(cmd);
                        }
                    }
                    else
                    {
                        if (command == "shutdown")
                        {
                            // this is actually the "exit" command
                            command = "exit";
                        }

                        SendCommand(connection, command);
                    }
                }
            }
        }

        private static void SendCommand(RCON connection, string command)
        {
            command = command.ToLower().Trim();

            var noResponseCommands = new List<string>() {
                "addbluforbots", "addbots", "addnamedbot", "addopforbots", "addteambots",
                "removeallbots", "removebluforbots", "removeopforbots", "removeteambots",
                "say"
            };

            var maybeResponseCommands = new List<string>() {
                "servertravel"
            };

            Action<string> AppendText = (text) =>
            {
                Console.WriteLine(text);
            };

            if (connection != null && connection.Connected)
            {
                var parts = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    if (noResponseCommands.Contains(parts.First()))
                    {
                        // send command - expect no response
                        connection.SendCommandNoResponse(command, AppendText);
                    }
                    else if (maybeResponseCommands.Contains(parts.First()))
                    {
                        // send command - expect no response or maybe an error response
                        connection.SendCommandIgnoreTimeout(command, AppendText);
                    }
                    else
                    {
                        // send command - expect response
                        switch (parts.First())
                        {
                            default:
                                // generic
                                connection.SendCommand(command, AppendText);
                                break;
                        }
                    }
                }
            }

        }
    }
}
