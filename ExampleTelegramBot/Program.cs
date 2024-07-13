using ExampleTelegramBot;
using ExampleTelegramBot.Models;
using Newtonsoft.Json;
using Telegram.Bot.Core.Utilities;

public class Program()
{
    public static async Task Main(string[] args)
    {
        Settings _settings = null;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Logger.Current = new Logger(FilesManager.LogFile, FilesManager.ErrorsFile);

        if (!File.Exists(FilesManager.SettingsFile))
        {
            Settings settings = new Settings()
            {
                AdminKey = string.Empty,
                ConnectionString = string.Empty,
                BotToken = string.Empty,

            };

            File.WriteAllText(FilesManager.SettingsFile, JsonConvert.SerializeObject(settings, Formatting.Indented));

            Logger.Current.LogInfo($"Заполните файл {FilesManager.SettingsFile} и перезапустите программу");
            Logger.Current.LogInfo($"Для завершения работы программы нажмите любую клавишу...");
            Console.ReadKey();
            return;
        }

        Settings.Current = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(FilesManager.SettingsFile));
        Logger.Current.LogSuccess("Настройки загружены");

        using (var context = new DataBaseContext())
        {
            context.Database.EnsureCreated();
        }
        Logger.Current.LogSuccess("БД создана");

        var bot = new Bot(Settings.Current.BotToken);
        bot.Start();

        Logger.Current.LogSuccess("Бот запущен");

        Logger.Current.LogSuccess("Все готово. Пользуйтесь");
        Logger.Current.LogInfo($"Если запускаете первый раз, пропишите в боте /start {Settings.Current.AdminKey}" +
            $"\nЭто сделает пользователя администратором, откроется доступ ко всем командам");
        Console.ReadLine();
    }
}