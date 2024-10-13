using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SauRCON.SaveGame
{
    public static class SauronLink
    {
        public static bool UsingTestData { get; set; }

        private static readonly TimeSpan sMaxAllowedAge = TimeSpan.FromSeconds(5);

        private const string kSaveGamesPath = @"%LOCALAPPDATA%\HarshDoorstop\Saved\SaveGames\";
        
        private const string kSauronOutSav = "SauronOut.sav";
        private const string kImportBatchFile = @"uesave-x86_64-pc-windows-msvc\import.bat";

        private const string kSauronInSav = "SauronIn.sav";
        private const string kExportBatchFile = @"uesave-x86_64-pc-windows-msvc\export.bat";

        public delegate void OnLinkStatus_delegate(bool state);
        public static event OnLinkStatus_delegate OnInLinkStatus;
        public static event OnLinkStatus_delegate OnOutLinkStatus;

        public delegate bool OnValidateInputObject_delegate(SauronOutObject sauronOutObject);
        public static OnValidateInputObject_delegate OnValidateInputObject;

        public delegate void OnPlayerMapping_delegate(Dictionary<int, string> plaeyerMapping);
        public static event OnPlayerMapping_delegate OnPlayerMapping;

        public delegate void OnSelectedPlayer_delegate(int id, DateTime at);
        public static event OnSelectedPlayer_delegate OnSelectedPlayer;

        public static void SauronImport()
        {
            try
            {
                // convert SAV file into JSON
                var file = kSaveGamesPath + kSauronOutSav;
                var result = UESaveUtil.ExecuteCommand(kImportBatchFile, file, out string jsonText, true);
                if (result)
                {
                    // convert JSON into SauronOutObject
                    var jsonMapper = JsonConvert.DeserializeObject<JsonMapper>(jsonText);
                    var obj = SauronOutObject.FromMapper(jsonMapper);

                    // check object isn't too old to be relevant
                    var objectValid = obj.Age <= sMaxAllowedAge;

                    // validate object
                    objectValid &= OnValidateInputObject != null ? OnValidateInputObject(obj) : true;

                    if (objectValid || UsingTestData)
                    {
                        // apply name look-up
                        OnPlayerMapping?.Invoke(obj.PlayerMapping);

                        // apply player selection
                        OnSelectedPlayer?.Invoke(obj.SelectedPlayerId, obj.SelectedPlayerAt);

                        OnInLinkStatus?.Invoke(true);
                    }
                    else
                    {
                        OnInLinkStatus?.Invoke(false);
                    }
                }
            }
            catch { /* TODO: provide feedback that import is working or not */ }
        }

        // TODO: SauronExport()
    }
}
