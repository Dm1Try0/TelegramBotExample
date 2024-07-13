using ExampleTelegramBot.Models;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExampleTelegramBot
{
    internal class CommandHandler
    {
        private TelegramBotClient _bot;
        internal async Task Message(ITelegramBotClient botClient, Message message)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.GetByTgId(message.Chat.Id);
                if (user is null)
                {
                    var usercreate = new UserData()
                    {
                        Username = message.Chat.Username,
                        TelegramId = message.Chat.Id,
                        TgName = message.Chat.FirstName,
                    };
                    db.Add(usercreate);
                }
            }
            if (message.Text.StartsWith("/start"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Hello World");
                return;
            }
            switch (message.Text)
            {
                case "/start":
                default: SendMessage(message.Chat.Id, "UnknownCommand"); break;
            }
        }

        internal async Task CallBack(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.GetByTgId(callbackQuery.Message.Chat.Id);
                if (user is null)
                {
                    var usercreate = new UserData()
                    {
                        Username = callbackQuery.Message.Chat.Username,
                        TelegramId = callbackQuery.Message.Chat.Id,
                        TgName = callbackQuery.Message.Chat.FirstName,
                    };
                    db.Add(usercreate);
                }
            }
        }
        internal async Task SendMessage(long chatId, string text)
        {
            await _bot.SendTextMessageAsync(chatId, text);
            return;
        }
    }
}
