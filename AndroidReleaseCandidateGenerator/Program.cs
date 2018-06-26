using System;

namespace AndroidReleaseCandidateGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            ui.StartPocSigner();
        }
    }
}
