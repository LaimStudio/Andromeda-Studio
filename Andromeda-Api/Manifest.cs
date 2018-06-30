namespace AndromedaApi
{
    public class Manifest
    {
        private string name;
        private string version;
        private string[] components;

        public string Name { get => name; set => name = value; }
        public string Version { get => version; set => version = value; }
        public string[] Components { get => components; set => components = value; }
    }
}