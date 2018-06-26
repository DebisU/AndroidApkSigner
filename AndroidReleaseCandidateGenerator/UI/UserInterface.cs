using System;

namespace AndroidReleaseCandidateGenerator
{
    public class UserInterface
    {
        private StringsProvider stringProvider;

        public UserInterface()
        {
            stringProvider = new StringsProvider();
        }

        public void StartPocSigner()
        {
            bool finish = false;
            do
            {
                Console.WriteLine("Is it Tablet or Mobile app? T/M (type exit(e) to finish the program or clear(c) to clear console.)");
                string answer = Console.ReadLine().ToLower();
    
                if (answer.Equals("t") || answer.Equals("tablet"))
                {
                    CreateReleaseCandidateForPocTablet();
                }
                else if (answer.Equals("m") || answer.Equals("mobile"))
                {
                    CreateReleaseCandidateForPocMobile();
                }
                else if (answer.Equals("exit") || answer.Equals("e"))
                {
                    finish = true;
                }
                else if (answer.Equals("clear") || answer.Equals("c"))
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid answer. should be tablet(T), mobile(M), Clear(C) or Exit(E).");
                }
            } while (!finish);

            Console.WriteLine("See you!");
            Console.ReadLine();
        }


        private void CreateReleaseCandidateForPocMobile()
        {
            MobileReleaseCandidateGenerator releaseCandidateGenerator = new MobileReleaseCandidateGenerator(stringProvider);

            Console.WriteLine("Step 1: Creating assemble release...");
            releaseCandidateGenerator.GenerateAssembleRelease();
            releaseCandidateGenerator.fileInfo = stringProvider.ReleaseCandidateNameData(false);
            Console.WriteLine("Step 2: Using zip align...");
            releaseCandidateGenerator.AlignApp();
            Console.WriteLine("Step 3: Using apk signer...");
            releaseCandidateGenerator.SignApp();
            Console.WriteLine("Step 4: Verify signature...");
            releaseCandidateGenerator.VerifySignature();
            Console.WriteLine("Step 5: Make sha512...");
            releaseCandidateGenerator.GenerateSha512();
        }

        private void CreateReleaseCandidateForPocTablet()
        {
            TabletReleaseCandidateGenerator releaseCandidateGenerator = new TabletReleaseCandidateGenerator(stringProvider);

            Console.WriteLine("Step 1: Creating assemble release...");
            releaseCandidateGenerator.GenerateAssembleRelease();
            releaseCandidateGenerator.fileInfo = stringProvider.ReleaseCandidateNameData(true);
            Console.WriteLine("Step 2: Using zip align...");
            releaseCandidateGenerator.AlignApp();
            Console.WriteLine("Step 3: Using apk signer...");
            releaseCandidateGenerator.SignApp();
            Console.WriteLine("Step 4: Verify signature...");
            releaseCandidateGenerator.VerifySignature();
            Console.WriteLine("Step 5: Make sha512...");
            releaseCandidateGenerator.GenerateSha512();
        }
    }
}
