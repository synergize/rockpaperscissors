using System;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;

namespace rockpaperscissors
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


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
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            Client.Ready += Client_Ready;
            Client.Log += Log;

            string Token = System.Environment.GetEnvironmentVariable("RPS_TOKEN");
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();
            await Task.Delay(-1);            
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
            return Task.CompletedTask;
        }

        private async Task Client_MessageRecieved(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasCharPrefix('!', ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos, null);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");




        }
        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Rock Paper Scissors Bot!", "https://discordapp.com/developers", ActivityType.Listening);
        }
    }
}
