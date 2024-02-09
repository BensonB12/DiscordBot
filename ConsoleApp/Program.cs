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
        _client = new DiscordSocketClient(new DiscordSocketConfig() { 
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            LogLevel = LogSeverity.Info,
        });
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, "YOUR-TOKEN-HERE");
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private Task ReadyAsync()
    {
        Console.WriteLine("Bot is connected and ready!");
        return Task.CompletedTask;
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        Console.WriteLine($"Message received from {message.Author.Username}: {message.Content}");

        if (message.Author.IsBot)
        {
            Console.WriteLine("Message from a bot. Ignoring.");
            return;
        }

        Console.WriteLine($"Author: {message.Author}");

        if (message.Content.ToLower() == "hello")
        {
            await message.Channel.SendMessageAsync("Hello, Discord!");
        }
    }

    public static string FindFile(string fileName)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (true)
        {
            var testPath = Path.Combine(directory.FullName, fileName);
            if (File.Exists(testPath))
            {
                return testPath;
            }
            if (directory.FullName == directory.Root.FullName)
            {
                throw new FileNotFoundException($"I looked for {fileName} in every folder from {Directory.GetCurrentDirectory()} to {directory.Root.FullName} and couldn't find it.");
            }
            directory = directory.Parent;
        }
    }
}