using Discord;
using Discord.WebSocket;
using DotNetEnv;

class Program
{
    private DiscordSocketClient _client;

    static async Task Main(string[] args)
    {
      var program = new Program();
        await program.RunBotAsync();
    }

    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.MessageReceived += MessageReceivedAsync;

        //Developement only
        Env.Load();

        var token = BotConfiguration.BotToken;


        if (token != null)
        {
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        else
        {
            Console.WriteLine("Failed to read bot token from envirnment");
        }

    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Content.ToLower() == "hello")
        {
            await message.Channel.SendMessageAsync("Hello, Discord!");
        }
    }
}