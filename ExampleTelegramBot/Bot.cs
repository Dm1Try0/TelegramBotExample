using Telegram.Bot;
using Telegram.Bot.Core.Utilities;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExampleTelegramBot
{
    internal class Bot
    {
        private TelegramBotClient _bot;
        private CommandHandler _handler;
        public Bot(string token)
        {
            _handler = new CommandHandler();
            _bot = new TelegramBotClient(token);
        }
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Logger.Current.LogException(exception, additionalInfo: "HandlePollingError");
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message && update?.Message?.Text != null ^ update?.Message?.Photo != null ^ update?.Message?.Video != null)
                {
                    await HandleMessage(_bot, update.Message);

                    return;
                }

                if (update.Type == UpdateType.CallbackQuery)
                {
                    await HandleCallbackQuery(botClient, update.CallbackQuery);

                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Current.LogException(ex, additionalInfo: "HandleUpdate");
            }
        }
        private async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            await _handler.Message(botClient, message);
        }
        private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await _handler.CallBack(botClient, callbackQuery);
        }
        public void Start()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);
            var allowedUpdates = new[] { UpdateType.CallbackQuery, UpdateType.Message };
        }
    }
}
