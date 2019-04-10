using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace rockpaperscissors.Core.Commands
{
    public class RockPaperScissorsCommands : ModuleBase<SocketCommandContext>
    {
        public List<int> counter = new List<int>();

        bool boolean = true;

        [Command("void")]
        public async Task StartGame2()
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
            Embed.AddField("User Input", Input);
            boolean = false;
            try
            {
                counter.Add(1);
                for (int i = 0; i < counter.Count; i++)
                {
                    Embed.AddField("Counter", counter[i]);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }


            await Context.Channel.SendMessageAsync("", false, Embed.Build());

        [Command("play")]
        public async Task StartGame()
        {
            //Player PlayerOne = new Player(Context.User.Username);

        }
        ///<summary>
        /// Checks if our JSON file exists and returns true or false.
        /// </summary>
        public bool CodeExists()
        {
            return File.Exists(BaseCodesFile);
        }
        ///<summary>
        /// Checks if Directory ..\netcoreapp2.2\Data exists. If it doesn't, we create the directory and a blank BaseCodes.Json file
        /// If the directory exists but the BaseCodes file doesn't, we create a blank JSON file.
        /// </summary>
        public void CheckDirectory(string file)
        {
            if (!Directory.Exists(file))
            {
                //If the directory for our basecode json file doesn't exist we create it along with the json file. 
                DirectoryInfo dir = Directory.CreateDirectory(file);
                var CreateFile = File.Create(BaseCodesFile);
                CreateFile.Close();
            }
            else if (!CodeExists())
            {
                //if the directory exists but the file doesn't, we create the file. 
                var CreateFile = File.Create(BaseCodesFile);
                CreateFile.Close();
            }
        }
        ///<summary>
        /// Returns a list of <see cref="CodeLists"/> objects from a file locate in the passed in "File" location.
        /// </summary>
        public List<CodeLists> ReadFromJson(string file)
        {
            List<CodeLists> ListOfServers = new List<CodeLists>();
            file = File.ReadAllText(file);
            try
            {
                JsonConvert.PopulateObject(file, ListOfServers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ListOfServers;
        }
        ///<summary>
        /// Saves a passed in list of <see cref="CodeLists"/> to a JSON file named BaseCodes.
        /// </summary>
        public void WriteToJson(List<CodeLists> obj)
        {
            using (StreamWriter file = File.CreateText(BaseCodesFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }

        }
        ///<summary>
        /// Pulls in a list of <see cref="CodeLists"/> and formats a JSON structure with data provided from the user. 
        /// </summary>
        public void NewServerEntry(List<CodeLists> obj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.WritePropertyName("CodesList");
                writer.WriteStartArray();
                writer.Formatting = Formatting.Indented;
                foreach (var x in obj)
                {
                    writer.WriteStartObject();

                    writer.WritePropertyName("ServerID");
                    writer.WriteValue(x.ServerID);

                    writer.WritePropertyName("CodeEntry");
                    writer.WriteRawValue(x.CodeEntry.ToString());

                    writer.WritePropertyName("RoleName");
                    writer.WriteRawValue(x.Role);
                    writer.WriteEndObject();

                }
                writer.WriteEndArray();
            }
            using (StreamWriter file = File.CreateText(BaseCodesFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
                int x = 0;
            }
        }

    }


    
}
