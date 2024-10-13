using System.Diagnostics;

namespace SauRCON.SaveGame
{
    internal class UESaveUtil
    {
        public static bool ExecuteCommand(string command, string arguments, out string console, bool onlyRawOutput = false)
        {
            var output = onlyRawOutput ? string.Empty : "Executing ...\n";

            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + $"{command} {arguments}");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => output += e.Data;
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => output += e.Data;
            process.BeginErrorReadLine();

            process.WaitForExit();

            var rv = process.ExitCode == 0;

            output += onlyRawOutput ? string.Empty : $"\nExitCode: {process.ExitCode}";
            process.Close();

            console = output;

            return rv;
        }
    }
}
