using System.Diagnostics;

namespace AndroidReleaseCandidateGenerator
{
    public static class ProcessProvider
    {
        public static Process OpenAndRetrieveCmdProcess()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            return cmd;
        }


        public static Process OpenAndRetrieveBashProcess()
        {
            Process bash = new Process();
            bash.StartInfo.FileName = @"D:\cygwin64\bin\bash.exe";
            bash.StartInfo.RedirectStandardInput = true;
            bash.StartInfo.RedirectStandardOutput = true;
            bash.StartInfo.CreateNoWindow = true;
            bash.StartInfo.UseShellExecute = false;
            bash.Start();

            return bash;
        }


        public static string CloseProcessAndRetrieveOutput(Process cmd)
        {
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit(30);
            return cmd.StandardOutput.ReadToEnd();
        }
    }
}
