using CounterStrikeSharp.API;
using System.Reflection;
using Tomlyn.Model;
using Tomlyn;
using CounterStrikeSharp.API.Core.Translations;

namespace MenuManager
{
    public static class Config
    {
        public static Cfg ConfigData { get; set; } = new Cfg();

        public static void Load()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "";
            string cfgPath = $"{Server.GameDirectory}/csgo/addons/counterstrikesharp/configs/plugins/{assemblyName}";

            LoadConfig($"{cfgPath}/config.toml");
        }

        private static void LoadConfig(string configPath)
        {
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            string configText = File.ReadAllText(configPath);
            TomlTable model = Toml.ToModel(configText);

            string commandName = model.TryGetValue("CommandName", out var cmd) ? cmd.ToString()! : "css_menu";
            string messageTag = model.TryGetValue("MessageTag", out var tag) ? tag.ToString()! : "[MenuManager]";
            Config_MenuOptions menuOptions = new Config_MenuOptions();

            if (model.TryGetValue("MenuOptions", out var menuOptionsSection))
            {
                foreach (var entry in (TomlTable)menuOptionsSection)
                {
                    var optionTable = (TomlTable)entry.Value;
                    menuOptions.Options.Add(new MenuOption
                    {
                        Name = optionTable["Name"].ToString()!,
                        Command = optionTable["Command"].ToString()!,
                        OpeningMessage = optionTable.TryGetValue("OpeningMessage", out var msg)
                ? StringExtensions.ReplaceColorTags(msg.ToString()!)
                : "Opening {option}..."
                    });
                }
            }

            ConfigData = new Cfg
            {
                CommandName = commandName,
                MenuOptions = menuOptions
            };
        }

        public class Cfg
        {
            public string CommandName { get; set; } = "css_menu";
            public Config_MenuOptions MenuOptions { get; set; } = new();
        }

        public class Config_MenuOptions
        {
            public List<MenuOption> Options { get; set; } = new();
        }

        public class MenuOption
        {
            public string Name { get; set; } = string.Empty;
            public string Command { get; set; } = string.Empty;
            public string OpeningMessage { get; set; } = "Opening {option}...";
        }
    }
}
