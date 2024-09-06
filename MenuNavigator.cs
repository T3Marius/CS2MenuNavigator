using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Core;
using WASDSharedAPI;

namespace MenuNavigator
{
    public class AllInOne : BasePlugin
    {
        public override string ModuleName => "MenuNavigator";
        public override string ModuleVersion => "1.0.1";
        public override string ModuleAuthor => "T3Marius";

        public static IWasdMenuManager? MenuManager;

        public override void Load(bool hotReload)
        {
            Config.Load();


            foreach (var menu in Config.ConfigData.Menus)
            {
                AddCommand($"css_{menu.CommandName}", $"Opens {menu.MenuName}", (player, info) => OpenMenu(player, menu));
            }
        }

        public IWasdMenuManager? GetMenuManager()
        {
            if (MenuManager == null)
                MenuManager = new PluginCapability<IWasdMenuManager>("wasdmenu:manager").Get();

            return MenuManager;
        }

        public void OpenMenu(CCSPlayerController? player, Config.Menu menu)
        {
            var manager = GetMenuManager();
            if (manager == null)
                return;

            IWasdMenu wasdMenu = manager.CreateMenu(menu.MenuName);
            AddOptionsToMenu(wasdMenu, menu.MenuOptions);

            manager.OpenMainMenu(player, wasdMenu);
        }

        private void AddOptionsToMenu(IWasdMenu menu, Dictionary<string, Config.MenuOption> options)
        {
            var manager = GetMenuManager();
            if (manager == null)
                return;

            foreach (var option in options.Values)
            {
                if (option.SubOptions.Any())
                {
                    IWasdMenu subMenu = manager.CreateMenu(option.Name);
                    AddOptionsToMenu(subMenu, option.SubOptions.ToDictionary(sub => sub.Name));
                    subMenu.Prev = menu.Add(option.Name, (p, opt) => manager.OpenSubMenu(p, subMenu));
                }
                else
                {
                    menu.Add(option.Name, (p, opt) =>
                    {
                        p.ExecuteClientCommandFromServer(option.Command);
                        p.PrintToChat($"{option.OpeningMessage.Replace("{option}", option.Name)}");
                        manager.CloseMenu(p);
                    });
                }
            }
        }
    }
}