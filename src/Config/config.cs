using System.Reflection;
using Tomlyn.Model;
using Tomlyn;
using CounterStrikeSharp.API;

namespace MenuNavigator
{
    public static class Config
    {
        public static MenuConfigData ConfigData { get; private set; } = new MenuConfigData();

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

            // Load Menus
            if (model.ContainsKey("Menus") && model["Menus"] is TomlTableArray menusArray)
            {
                foreach (TomlTable menuTable in menusArray)
                {
                    var menu = new Menu
                    {
                        CommandName = menuTable["CommandName"]?.ToString() ?? "unknown",
                        MenuName = menuTable["MenuName"]?.ToString() ?? "Unnamed Menu"
                    };

                    if (menuTable.ContainsKey("MenuOptions") && menuTable["MenuOptions"] is TomlTable menuOptionsTable)
                    {
                        menu.MenuOptions = ParseMenuOptions(menuOptionsTable);
                    }

                    ConfigData.Menus.Add(menu);
                }
            }
        }

        private static Dictionary<string, MenuOption> ParseMenuOptions(TomlTable menuOptionsTable)
        {
            var menuOptions = new Dictionary<string, MenuOption>();

            foreach (var optionName in menuOptionsTable.Keys)
            {
                var optionTable = menuOptionsTable[optionName] as TomlTable;
                if (optionTable == null) continue;

                var menuOption = new MenuOption
                {
                    Name = optionTable["Name"]?.ToString() ?? "Unnamed Option",
                    Command = optionTable["Command"]?.ToString() ?? "",
                    OpeningMessage = optionTable["OpeningMessage"]?.ToString() ?? ""
                };

                // Check for SubOptions
                if (optionTable.ContainsKey("SubOptions") && optionTable["SubOptions"] is TomlTableArray subOptionsArray)
                {
                    foreach (TomlTable subOptionTable in subOptionsArray)
                    {
                        var subOption = new MenuOption
                        {
                            Name = subOptionTable["Name"]?.ToString() ?? "Unnamed SubOption",
                            Command = subOptionTable["Command"]?.ToString() ?? "",
                            OpeningMessage = subOptionTable["OpeningMessage"]?.ToString() ?? ""
                        };
                        menuOption.SubOptions.Add(subOption);
                    }
                }

                menuOptions.Add(optionName, menuOption);
            }

            return menuOptions;
        }

        public class MenuConfigData
        {
            public List<Menu> Menus { get; set; } = new List<Menu>();
        }

        public class Menu
        {
            public string CommandName { get; set; } = string.Empty;
            public string MenuName { get; set; } = string.Empty;
            public Dictionary<string, MenuOption> MenuOptions { get; set; } = new Dictionary<string, MenuOption>();
        }

        public class MenuOption
        {
            public string Name { get; set; } = string.Empty;
            public string Command { get; set; } = string.Empty;
            public string OpeningMessage { get; set; } = string.Empty;
            public List<MenuOption> SubOptions { get; set; } = new List<MenuOption>();
        }
    }
}
