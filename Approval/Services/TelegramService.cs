using Approval;
using Approval.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Approval.Services
{
	public class TelegramService : IBotApproval
	{
		public async Task SendApproveOrder(int chat, string message)
		{
			await TelegramBot.Helper.SendTextMessageAsync(chat, message, replyMarkup: GetApprove);
			string res=await TelegramBot.GetCallbackData();

			await SendMessageAllUser("Order " + res);
		}

		public async Task SendMessageAllUser(string message)
		{
			foreach (var chat in TelegramBot.ChatList)
			{
				await TelegramBot.Helper.SendTextMessageAsync(chat, message);
			}
		}

		public async Task SendMessageToUser(string message)
		{
			throw new System.NotImplementedException();
		}

		IReplyMarkup GetApprove
		{
			get
			{
				List<InlineKeyboardButton>[] result = new List<InlineKeyboardButton>[]
					{
					new List<InlineKeyboardButton>()
					{
						new InlineKeyboardButton("Approve")
						{
							CallbackData= "approved"
						},
						new InlineKeyboardButton("Reject")
						{
							CallbackData= "rejected"
						}
					}
					};
				InlineKeyboardMarkup markup= new InlineKeyboardMarkup(result);
				return markup;
			}
			
		}
	}
}



 