namespace AndroidReleaseCandidateGenerator
{
    interface IReleaseCandidateGenerator
    {
        /* Steps:
         * 1: gradlew assembleRelease // assembleTabletRelease. V
         * 2: Zipalign. V
         * 3: Apksigner. V
         * 4: Verify apksigner. V
         * 5: Generate Sha512. V
         */
        void GenerateAssembleRelease();
        void AlignApp();
        void SignApp();
        void VerifySignature();
        void GenerateSha512();
    }
}
