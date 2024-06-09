namespace ChainTelegramBot.Services;

public class JugRecommendationService
{
    public string GetRecommendation(string lang, int experience) => (lang.ToLower(), experience) switch
    {
        ("c#", <=3) => "Простой доклад на DotNext",
        ("c#", >3) => "Сложный доклад на DotNext",
        ("java", <=3) => "Простой доклад на Joker",
        ("java", >3) => "Сложный доклад на Joker",
        _ => "Универсальный доклад для всех"
    };
}