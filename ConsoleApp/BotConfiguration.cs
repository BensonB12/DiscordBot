public static class BotConfiguration
{
    public static string? BotToken => Environment.GetEnvironmentVariable("TOKEN");
}