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
    }
}
