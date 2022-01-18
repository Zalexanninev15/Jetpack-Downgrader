namespace JetpackGUI
{
    public static class Data
    {
        public static string JetpackDowngraderVersion { get; } = "2.4.0.2";
        public static string PathToGame { get; set; }
        public static bool DebugMode { get; set; }
    }

    public class ModsData
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public string Logo { get; set; }
        public string Screenshot1 { get; set; }
        public string Screenshot2 { get; set; }
        public string File { get; set; }
    }
}