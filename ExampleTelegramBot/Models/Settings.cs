namespace ExampleTelegramBot.Models
{
    internal class Settings
    {
        public static Settings Current;
        public string BotToken { get; set; }
        public string AdminKey { get; set; }
        public string ConnectionString { get; set; }
    }
}
