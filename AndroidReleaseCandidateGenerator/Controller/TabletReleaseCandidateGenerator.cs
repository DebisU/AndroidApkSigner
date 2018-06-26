using System;
using System.Diagnostics;

namespace AndroidReleaseCandidateGenerator
{
    public class TabletReleaseCandidateGenerator : IReleaseCandidateGenerator
    {
        public AndroidOutputVersioning fileInfo;
        private StringsProvider stringProvider;

        public TabletReleaseCandidateGenerator(StringsProvider stringProvider)
        {
            this.stringProvider = stringProvider;
        }

        public void GenerateAssembleRelease()
        {
            Process cmd = ProcessProvider.OpenAndRetrieveCmdProcess();

            cmd.StandardInput.WriteLine("d:");
            cmd.StandardInput.WriteLine("cd " + stringProvider.pocRootFolderPath);
            cmd.StandardInput.WriteLine("gradlew assembleTabletRelease");

            string output = ProcessProvider.CloseProcessAndRetrieveOutput(cmd);
            Console.WriteLine(output);
        }


        public void AlignApp()
        {
            Process cmd = ProcessProvider.OpenAndRetrieveCmdProcess();

            cmd.StandardInput.WriteLine("d:");
            cmd.StandardInput.WriteLine("cd " + stringProvider.androidToolsPath);

            string refinedAlignCommand = stringProvider.alignCommand
                .Replace("{unaligned}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + ".apk")
                .Replace("{aligned}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + "_aligned.apk");
            cmd.StandardInput.WriteLine(refinedAlignCommand);

            string output = ProcessProvider.CloseProcessAndRetrieveOutput(cmd);
            Console.WriteLine(output);
        }


        public void SignApp()
        {
            Process cmd = ProcessProvider.OpenAndRetrieveCmdProcess();

            cmd.StandardInput.WriteLine("d:");
            cmd.StandardInput.WriteLine("cd " + stringProvider.androidToolsPath);

            if (Environment.GetEnvironmentVariable("signerTablet") == null)
            {
                throw new Exception("Load environment variables.");
            }

            string refinedSignCommand = stringProvider.signCommand;
            refinedSignCommand = refinedSignCommand.Replace("{keystore_file_name}", stringProvider.keyStoreFileTablet);
            refinedSignCommand = refinedSignCommand.Replace("{envVar}", "signerTablet");
            refinedSignCommand = refinedSignCommand.Replace("{signed}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + "_aligned_signed.apk");
            refinedSignCommand = refinedSignCommand.Replace("{aligned}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + "_aligned.apk");
            cmd.StandardInput.WriteLine(refinedSignCommand);


            string output = ProcessProvider.CloseProcessAndRetrieveOutput(cmd);
            Console.WriteLine(output);
        }


        public void VerifySignature()
        {
            Process cmd = ProcessProvider.OpenAndRetrieveCmdProcess();

            cmd.StandardInput.WriteLine("d:");
            cmd.StandardInput.WriteLine("cd " + stringProvider.androidToolsPath);


            string refinedVerifySignCommand = stringProvider.verifySignatureCommand;
            refinedVerifySignCommand = refinedVerifySignCommand.Replace("{outputLocation}", stringProvider.fullTabletOutputPathLocation);
            refinedVerifySignCommand = refinedVerifySignCommand.Replace("{signedAligned}", fileInfo.ToString() + "_aligned_signed.apk");
            cmd.StandardInput.WriteLine(refinedVerifySignCommand);


            string output = ProcessProvider.CloseProcessAndRetrieveOutput(cmd);
            Console.WriteLine(output);
        }


        public void GenerateSha512()
        {
            Process bash = ProcessProvider.OpenAndRetrieveBashProcess();

            bash.StandardInput.WriteLine("d:");

            string refinedSha512Command = stringProvider.sha512Command;
            refinedSha512Command = refinedSha512Command.Replace("{signedAligned}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + "_aligned_signed.apk");
            refinedSha512Command = refinedSha512Command.Replace("{sha512}", stringProvider.fullTabletOutputPathLocation + fileInfo.ToString() + "_aligned_signed.apk.sha512");
            refinedSha512Command = refinedSha512Command.Replace("\\", "/");

            bash.StandardInput.WriteLine(refinedSha512Command);
            stringProvider.CopyToClipBoard(refinedSha512Command);
            Console.WriteLine("This command has been copied(because the sha215 gen does not work in an external process)");

            string output = ProcessProvider.CloseProcessAndRetrieveOutput(bash);
            Console.WriteLine(output);
        }
    }
}
