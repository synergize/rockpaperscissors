using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace rockpaperscissors
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug

            });

            Client.MessageReceived += Client_MessageRecieved;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            string Token = System.Environment.GetEnvironmentVariable("BOT_TOKEN");
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_MessageRecieved(SocketMessage arg)
        {
           
        }
        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Rock Paper Scissors Bot!", "https://discordapp.com/developers", StreamType.NotStreaming);
        }
    }
}
