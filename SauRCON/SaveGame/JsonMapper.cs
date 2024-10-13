using System.Collections.Generic;
using System.Linq;

namespace SauRCON.SaveGame
{
    public class JsonMapper
    {
        public Header header { get; set; }
        public Root root { get; set; } 
        public int[] extra { get; set; }

        public PropertyPair GetProperty(string key)
        {
            try
            {
                if (root.properties.ContainsKey(key))
                {
                    return root.properties[key];
                }
                else
                {
                    var caseInsensitiveSearch = root.properties.FirstOrDefault(x => string.Compare(key, x.Key, true) == 0);
                    return caseInsensitiveSearch.Key != null ? caseInsensitiveSearch.Value : null;
                }
            }
            catch { /* lazy way of protecting against null components */ }

            return null;
        }
    }

    public class Header
    {
        public int magic { get; set; }
        public int save_game_version { get; set; }
        public PackageVersion package_version;
        public int engine_version_major { get; set; }
        public int engine_version_minor { get; set; }
        public int engine_version_patch { get; set; }
        public int engine_version_build { get; set; }
        public string engine_version { get; set; }
        public int custom_format_version { get; set; }
        public List<CustomFormat> custom_format { get; set; }
    }

    public class PackageVersion
    {
        public int? ue4 { get; set; }
        public int? ue5 { get; set; }
    };

    public class CustomFormat
    {
        public string id { get; set; }
        public int value { get; set; }
    }

    public class Root
    {
        public string save_game_type { get; set; }
        public Dictionary<string, PropertyPair> properties { get; set; }
    }

    public class PropertyPair
    {
        public Property Int { get; set; }
        public Property Str { get; set; }
        public PropertyMap Map { get; set; }
        public PropertyStruct Struct { get; set; }
    }

    public class Property
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class PropertyMap
    {
        public string id { get; set; }
        public string key_type { get; set; }    // e.g. IntProperty, StrProperty
        public string value_type { get; set; }  // e.g. IntProperty, StrProperty
        public List<MapItem> value { get; set; }
    }

    public class MapItem
    {
        public MapPair key { get; set; }
        public MapPair value { get; set; }
    }

    public class MapPair
    {
        public string Int { get; set; } // IntProperty
        public string Str { get; set; } // StrProperty
    }

    public class PropertyStruct
    {
        public string id { get; set; }
        public string struct_id { get; set; }
        public string struct_type { get; set; }
        public StructItem value { get; set; }
    }

    public class StructItem
    {
        public string DateTime { get; set; }    // DateTime
    }

}
