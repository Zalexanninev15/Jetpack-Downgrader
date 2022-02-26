namespace JetpackGUI
{
    public static class Data
    {
        public static string JetpackDowngraderVersion { get; } = "2.4.0.2";
        public static string PathToGame { get; set; }
        public static bool DebugMode { get; set; }
        public static string NewVersionDetector { get; } = "https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/Version.txt";
        public static string GitHubPing { get; } = "github.com";
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

    public static class Urls
    {
        public static string GitHub { get; } = "https://github.com/Zalexanninev15/Jetpack-Downgrader";
        public static string Release { get; } = "https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest";
        public static string GTAForums { get; } = "https://gtaforums.com/topic/969056-jetpack-downgrader";
        public static string GitHubAppUsage { get; } = "https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage";
        public static string GitHubAppAuthors { get; } = "https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#authors";
        public static string GitHubAppIssues { get; } = "https://github.com/Zalexanninev15/Jetpack-Downgrader/issues";
    }
}