using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using rockpaperscissors.Core.Data.Objects;

namespace rockpaperscissors.Core.Commands
{
    public class RockPaperScissorsCommands : ModuleBase<SocketCommandContext>
    {
        string FilePath = @"..\netcoreapp2.0\Data";
        string RPSPlayerFile = @"..\netcoreapp2.0\Data\Players.json";
        PlayerObject P1 = new PlayerObject(129804455964049408, 2, 4);
        List<PlayerObject> TestList = new List<PlayerObject>();
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

        }

        [Command("play")]
        public async Task StartGame()
        {
            CheckDirectory(FilePath);
            TestList.Add(P1);
            PlayerEntry(TestList);

        }
        ///<summary>
        /// Checks if our JSON file exists and returns true or false.
        /// </summary>
        public bool CodeExists()
        {
            return File.Exists(RPSPlayerFile);
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
                var CreateFile = File.Create(RPSPlayerFile);
                CreateFile.Close();
            }
            else if (!CodeExists())
            {
                //if the directory exists but the file doesn't, we create the file. 
                var CreateFile = File.Create(RPSPlayerFile);
                CreateFile.Close();
            }
        }
        ///<summary>
        /// Returns a list of <see cref="PlayerObject"/> objects from a file locate in the passed in "File" location.
        /// </summary>
        public List<PlayerObject> ReadFromJson(string file)
        {
            List<PlayerObject> ListOfServers = new List<PlayerObject>();
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
        /// Saves a passed in list of <see cref="PlayerObject"/> to a JSON file named BaseCodes.
        /// </summary>
        public void WriteToJson(List<PlayerObject> obj)
        {
            using (StreamWriter file = File.CreateText(RPSPlayerFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }

        }
        ///<summary>
        /// Pulls in a list of <see cref="PlayerObject"/> and formats a JSON structure with data provided from the user. 
        /// </summary>
        public void PlayerEntry(List<PlayerObject> obj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.WritePropertyName("Players");
                writer.WriteStartArray();
                writer.Formatting = Formatting.Indented;
                foreach (var x in obj)
                {
                    writer.WriteStartObject();

                    writer.WritePropertyName("PlayerID");
                    writer.WriteValue(x.PlayerID);

                    writer.WritePropertyName("PlayerWins");
                    writer.WriteRawValue(x.PlayerWins.ToString());

                    writer.WritePropertyName("PlayerLosses");
                    writer.WriteRawValue(x.PlayerLosses.ToString());
                    writer.WriteEndObject();

                }
                writer.WriteEndArray();
            }
            this.WriteToJson(obj);
        }

    }


    
}
