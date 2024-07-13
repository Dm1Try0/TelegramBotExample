namespace ExampleTelegramBot
{
    internal class FilesManager
    {
        private const string baseFolderName = "Settings";
        private const string settingsFileName = "settings.json";
        private const string logFileName = "log.txt";
        private const string errorsFileName = "errors.txt";
        public static string BaseFolder => Directory.CreateDirectory(baseFolderName).FullName;
        public static string SettingsFile => Path.Combine(BaseFolder, settingsFileName);
        public static string ErrorsFile => Path.Combine(BaseFolder, errorsFileName);
        public static string LogFile => Path.Combine(BaseFolder, logFileName);
    }
}
