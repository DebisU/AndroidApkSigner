namespace AndroidReleaseCandidateGenerator
{
    public class AndroidOutputVersioning
    {
        public string name;
        public string version;
        public string identifier;

        public override string ToString()
        {
            return name + "-" + version + "_" + identifier;
        }
    }
}
