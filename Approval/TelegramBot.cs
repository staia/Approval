using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Approval
{
    public class TelegramBot
    {
        const string TOKEN = "5878982485:AAFKTAUGr6AY6l4iWtmJGXJbb7yxCtsp2M0";  

        public static List<Chat> ChatList {get; set;}

        public static TelegramBotClient Helper { get; set; } // создаем параметр TelegramBotClient, который будет называться Helper

        static TelegramBot() //конуструктор, в котором должен проинициализироваться Helper и в TelegramBotClient передать токен с телеграм
        {
            ChatList= new List<Chat>();
            Helper = new TelegramBotClient(TOKEN);

            CancellationTokenSource source= new CancellationTokenSource();  
            CancellationToken cancel = source.Token;

            ReceiverOptions options = new ReceiverOptions() { AllowedUpdates = {}};   //   AllowedUpdates = { } слушаем все изменения

            Helper.StartReceiving(BotTakeMassage, BotTakeError, options, cancel); //начать прослушивания BotTakeMassage - полученея сообщения, BotTakeError - получение ошибки,
                                                                                  //options - параметр, где получаем все изменения и cancel для отмены.

            Helper.SendTextMessageAsync(840338962, "I was born");
        } 
            
            static async Task BotTakeMassage(ITelegramBotClient botClient, Update update, CancellationToken token)
            {
            if(update.Type == UpdateType.Message)
            {
                var current = update.Message.Chat;
                if(ChatList.FirstOrDefault(chat => chat.Id==current.Id)!= null)
                    {
                    await Helper.SendTextMessageAsync(update.Message.Chat.Id, "You already exist on the list");
                    }
                else
                    {
                    await Helper.SendTextMessageAsync(update.Message.Chat.Id, "You have been added to the list of users");
                    ChatList.Add(current);
                    }  
            }
                   else if(update.Type == UpdateType.CallbackQuery)
                    {
                await Helper.DeleteMessageAsync(update.CallbackQuery.From.Id, update.CallbackQuery.Message.MessageId);
                    responser.SetResult(update.CallbackQuery.Data);
				    //await Helper.SendTextMessageAsync(840338962, update.CallbackQuery.Data);
				    //responser.SetResult("yes");
			        }
            }

        public static TaskCompletionSource<string> responser = new TaskCompletionSource<string>();
        public static async Task<string> GetCallbackData()
        {
            string result = await responser.Task;
            responser = new TaskCompletionSource<string>();
            return result;
        }

            static async Task BotTakeError(ITelegramBotClient botClient, Exception ex, CancellationToken cancel)
            {
            }
        }
    }
