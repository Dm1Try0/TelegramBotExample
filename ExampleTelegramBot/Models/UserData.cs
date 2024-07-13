namespace ExampleTelegramBot.Models
{
    internal class UserData
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string Username { get; set; }
        public string TgName { get; set; }
        public bool IsUserAdmin { get; set; } = false;
    }
}
