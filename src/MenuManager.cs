using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Commands;
using WASDSharedAPI;

namespace MenuManager
{
    public class AllInOne : BasePlugin
    {
        public override string ModuleName => "MenuManager";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleAuthor => "T3Marius";

        public static IWasdMenuManager? MenuManager;

        public override void Load(bool hotReload)
        {
            Config.Load();
            foreach (var command in Config.ConfigData.CommandName.Split(','))
            {
                AddCommand($"css_{command}", "opens menu", Menu);
            }
            
        }
        public IWasdMenuManager? GetMenuManager()
        {
            if (MenuManager == null)
                MenuManager = new PluginCapability<IWasdMenuManager>("wasdmenu:manager").Get();

            return MenuManager; 
        }

        public void Menu(CCSPlayerController? player, CommandInfo? info)
        {
            var manager = GetMenuManager();
            if (manager == null)
                return;

            IWasdMenu menu = manager.CreateMenu("Menu");

            foreach (var option in Config.ConfigData.MenuOptions.Options)
            {
                menu.Add(option.Name, (p, opt) =>
                {
                    p.ExecuteClientCommandFromServer(option.Command);
                    p.PrintToChat($"{Localizer["prefix"]} {option.OpeningMessage.Replace("{option}", option.Name)}");
                    manager.CloseMenu(p);
                });
            }

            manager.OpenMainMenu(player, menu);
        }
    }
}
