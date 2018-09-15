using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace rockpaperscissors.Core.Commands
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //public List<int> counter = new List<int>();

        //bool boolean = true;

        //[Command("void")]
        //public async Task StartGame()
        //{
        //    await Context.Channel.SendMessageAsync("Rock, Paper, Scissor, Shoot!");

        //}
        //[Command("help")]
        //public async Task Help([Remainder]string Input = "None")
        //{
        //    EmbedBuilder Embed = new EmbedBuilder();
        //    Embed.WithAuthor("Game Instructions", Context.User.GetAvatarUrl());
        //    Embed.WithFooter($"The owner of this bot {Context.Guild.Owner.ToString()}");
        //    Embed.WithDescription($"Place holder description. \n [This is a hyperlink](https://discordapp.com/developers)");
        //    Embed.AddInlineField("User Input", Input);
        //    boolean = false;
        //    try
        //    {
        //        counter.Add(1);
        //        for (int i = 0; i < counter.Count; i++)
        //        {
        //            Embed.AddInlineField("Counter", counter[i]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex;
        //    }


        //    await Context.Channel.SendMessageAsync("", false, Embed.Build());

        [Command("play")]
        public async Task StartGame()
        {
            Player PlayerOne = new Player(Context.User.Username);

        }

    }

    public class Player : Commands
    {
        public Player(string name)
        {
            name = Context.User.Username;
        }

        public class GameObject : Player
        {
            public GameObject(string name) : base(name)
            {
                string playerName = name;
            }

        }
    } 
    
}
