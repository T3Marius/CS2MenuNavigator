# CS2MenuManager
This plugin is made for handling others menus and creating news one if you want. 
<img src="https://github.com/user-attachments/assets/fc982f47-4cca-47e4-9f64-f683dfeec1f9" alt="Image 1" width="300" />
<img src="https://github.com/user-attachments/assets/e9e984ab-82e1-4e98-a5e3-540faa76dd3b" alt="Image 2" width="300" />



# Config

```
[[Menus]]

CommandName = "menu"
MenuName = "Main Menu"

[Menus.MenuOptions]

    [Menus.MenuOptions.Store]
    Name = "Store"
    Command = "" #If the option is a sub menu, you don't need a command.
    OpeningMessage = "{red}Opening Store..."

    [[Menus.MenuOptions.Store.SubOptions]]
    Name = "Open Store"
    Command = "css_store"
    OpeningMessage = "Opening store..."

    [[Menus.MenuOptions.Store.SubOptions]]
    Name = "TeamBet"
    Command = "css_bet 100"
    OpeningMessage = "Opening TeamBets..."

    [[Menus.MenuOptions.Store.SubOptions]]
    Name = "Coinflip"
    Command = "css_coinflip 100"
    OpeningMessage = "Opening Coinflip..."

    [[Menus.MenuOptions.Store.SubOptions]]
    Name = "Cases"
    Command = "css_cases"
    OpeningMessage = "Opening cases..."

    [[Menus.MenuOptions.Store.SubOptions]]
    Name = "Trails"
    Command = "css_trails"
    OpeningMessage = "Opening cases..."
    # This is where store suboptions end.


    [Menus.MenuOptions.Models]
    Name = "Models"
    Command = ""
    OpeningMessage = "{green}Opening Models..."

    [[Menus.MenuOptions.Models.SubOptions]]
    Name = "Dutch"
    Command = "css_model Dutch"
    OpeningMessage = ""

    [[Menus.MenuOptions.Models.SubOptions]]
    Name = "Hitman"
    Command = "css_model Hitman"
    OpeningMessage = ""

    [[Menus.MenuOptions.Models.SubOptions]]
    Name = "Cortana"
    Command = "css_model Cortana"
    OpeningMessage = ""

    [[Menus.MenuOptions.Models.SubOptions]]
    Name = "Yuno"
    Command = "css_model Yuno"
    OpeningMessage = ""
    # Same with the models one. You don't need an opening message since the plugin aleardy has one.

    [Menus.MenuOptions.VIP]
    Name = "VIP"
    Command = "css_vip"
    OpeningMessage = "Accessing the VIP area..."
    #This is the VIPCore menu. and sadly we can't acces suboptions.


[[Menus]] #YOU NEED TO USE THIS WHENEVER YOU WANT TO CREATE ANOTHER MENU.

CommandName = "menu1" #Command to open the second menu !menu1
MenuName = "Menu1" #Name of the menu

[Menus.MenuOptions]

    [Menus.MenuOptions.Example]
    Name = "Example Option"
    Command = "example_command"
    OpeningMessage = "Executing example command..."

    [[Menus.MenuOptions.Example.SubOptions]] #Example of how to use suboptions.
    Name = "Example SubOption"
    Command = "example_subcommand"
    OpeningMessage = "Executing example subcommand..."
```
# Lang file (en.json)
```js
{
  "prefix": "{red}[MenuManager] ",
  "menu.name": "˗ˏˋ ★ ˎˊ˗ [Menu] ˗ˏˋ ★ ˎˊ˗"
}
```
[Buy me a coffee :)](https://paypal.me/vxaero?country.x=RO&locale.x=en_US)


