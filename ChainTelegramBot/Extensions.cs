using System.Text;
using ChainTelegramBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChainTelegramBot;

public static class Extensions
{
    public static long? GetUserId(this Update update)
    {
        return update.Message?.From?.Id ?? update.CallbackQuery?.From.Id;
    }

    public static InlineKeyboardButton AsButton(this Presentation presentation, bool visited)
    {
        StringBuilder btnText = new();
        btnText.Append(presentation.NameWithTime());

        if (visited)
        {
            btnText.Append(" \u2705");
        }

        string callbackData = "toggle_visited_" + presentation.Id;
        return InlineKeyboardButton.WithCallbackData(btnText.ToString(), callbackData);
    }

    public static string NameWithTime(this Presentation presentation)
    {
        return presentation.Time.ToString("HH:mm — ") + presentation.Name;
    }
}