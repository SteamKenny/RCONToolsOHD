#if NETSTANDARD2_0
using System;
using System.Threading.Tasks;
#endif
using CoreRCON.Parsers;

namespace CoreRCON
{
    public static class MoreExtensions
    {
        public static T SendCommandParsed<T>(this RCON connection, string command, Action<string> appendText, TimeSpan? overrideTimeout = null)
            where T : class, IParseable, new()
        {
            if (appendText != null)
            {
                appendText($">{command}");
            }

            string response = connection.SendCommandAsync(command, overrideTimeout).Result;
            response = response.Replace("\n\n", string.Empty);
            response = response.Replace("\n", Environment.NewLine);
            if (appendText != null)
            {
                appendText(response);
            }

            // See comment about TaskCreationOptions.RunContinuationsAsynchronously in SendComandAsync<string>
            var source = new TaskCompletionSource<T>();
            var instance = ParserHelpers.CreateParser<T>();
            var container = new ParserContainer
            {
                IsMatch = instance.IsMatch,
                Parse = instance.Parse,
            };

            if (!container.TryParse(response, out var parsed))
            {
                throw new FormatException("Failed to parse server response");
            }
            return (T)parsed;
        }

        public static void SendCommand(this RCON connection, string command, Action<string> appendText, TimeSpan? overrideTimeout = null)
        {
            if (appendText != null)
            {
                appendText($">{command}");
            }

            var response = connection.SendCommandAsync(command, overrideTimeout).Result;
            response = response.Replace("\n\n", string.Empty);
            response = response.Replace("\n", Environment.NewLine);

            if (appendText != null)
            {
                appendText(response);
            }
        }

        public static void SendCommandNoResponse(this RCON connection, string command, Action<string> appendText)
        {
            if (appendText != null)
            {
                appendText($">{command}");
            }
            
            try
            {
                connection.SendCommandAsync(command, TimeSpan.FromMilliseconds(1)).Wait();
            }
            catch { /* ignore the timeout error - this is to be expected */ }

            if (appendText != null)
            {
                appendText("(Not waiting for response)");
            }
        }

        public static void SendCommandIgnoreTimeout(this RCON connection, string command, Action<string> appendText)
        {
            if (appendText != null)
            {
                appendText($">{command}");
            }

            try
            {
                var response = connection.SendCommandAsync(command, TimeSpan.FromSeconds(5)).Result;
                response = response.Replace("\n\n", string.Empty);
                response = response.Replace("\n", Environment.NewLine);
                if (appendText != null)
                {
                    appendText(response);
                }
            }
            catch
            {
                /* ignore the timeout error - this could happen if no error is replied with */
                if (appendText != null)
                {
                    appendText("(Received no response)");
                }
            }
        }
    }
}
