using System;
using System.Collections.Generic;
using System.Linq;

namespace SauRCON.SaveGame
{
    public class SauronOutObject
    {
        // public properties
        public string ServerName { get; set; }
        public int SelectedPlayerId { get; set; }
        public DateTime SelectedPlayerAt { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<int, string> PlayerMapping = new Dictionary<int, string>();

        public static SauronOutObject FromMapper(JsonMapper mapper)
        {
            var rv = new SauronOutObject();

            // get server name
            var serverNameProperty = mapper.GetProperty("ServerName_0");
            if (serverNameProperty != null)
            {
                rv.ServerName = serverNameProperty.Str.value.Trim();
            }

            // convert time-stamp
            var timeStampProperty = mapper.GetProperty("TimeStamp_0");
            if (timeStampProperty != null)
            {
                rv.TimeStamp = new DateTime(long.Parse(timeStampProperty.Struct.value.DateTime));
            }

            // convert raw JSON map into a dictionary
            var playerMappingProperty = mapper.GetProperty("PlayerMapping_0");
            if (playerMappingProperty != null)
            {
                var playerMappingRaw = playerMappingProperty.Map.value;
                rv.PlayerMapping = playerMappingRaw.Select(x => new KeyValuePair<int, string>(int.Parse(x.key.Int), x.value.Str)).ToDictionary(y => y.Key, y => y.Value);
            }

            // convert selected player & time-stamp
            var selectedPlayerIdProperty = mapper.GetProperty("SelectedPlayerId_0");
            if (selectedPlayerIdProperty != null)
            {
                rv.SelectedPlayerId = int.Parse(selectedPlayerIdProperty.Int.value);
            }
            var selectedPlayerAtProperty = mapper.GetProperty("SelectedPlayerAt_0");
            if (selectedPlayerAtProperty != null)
            {
                rv.SelectedPlayerAt = new DateTime(long.Parse(selectedPlayerAtProperty.Struct.value.DateTime));
            }

            return rv;
        }

        public TimeSpan Age
        {
            get
            {
                // use time-stamp to calculate object age
                return DateTime.UtcNow - TimeStamp;
            }
        }
    }
}
