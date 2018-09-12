using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
namespace rockpaperscissors.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hellow world command")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }
        [Command("start")]
        public async Task StartGame()
        {
            await Context.Channel.SendMessageAsync("Rock, Paper, Scissor, Shoot!");
            
        }
        [Command("help")]
        public async Task Help([Remainder]string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Game Instructions", Context.User.GetAvatarUrl());
            Embed.WithFooter($"The owner of this bot {Context.Guild.Owner.ToString()}");
            Embed.WithDescription($"Place holder description. \n [This is a hyperlink](https://discordapp.com/developers)");
            Embed.AddInlineField("User Input", Input);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
