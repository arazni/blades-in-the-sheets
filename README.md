Hi all!

I made a character sheet web app for Blades in the Dark.

I'm not a UI designer by any stretch, just a full stack dev, so it isn't exactly the prettiest site, but it works. I designed it with the intention of my friends at the table using their phones to access the app, but it is responsive to non-mobile screens as well.

| ![home-page.png](https://user-images.githubusercontent.com/130337624/230923924-135ab590-0c84-4148-be60-f2a27cac7923.png) | 
|:--:| 
| The home page |

There are two major features: a character builder and a dynamic character sheet.

The page for creating a new character sheet guides you through the character creation process and helps you build the character according to the rules of the game. You're first taken to an intermediary page where you pick your playbook from a set of cards and little descriptions are given for each.

| ![creation.png](https://user-images.githubusercontent.com/130337624/230924278-23043922-014e-4546-8cac-66c22a94efd8.png) | 
|:--:| 
| Assigning action dots, contacts, and special abilities |

Once you've created the character, they show up in the list on the homepage. Then you're able to access the dynamic character sheet.

| ![health-card.png](https://user-images.githubusercontent.com/130337624/230924684-1f8a2a2d-6102-484a-9b60-3e5365212694.png) | 
|:--:| 
| Health card for managing your stress, trauma, harm, and automatically reducing your harms when a healing project is complete (and keeps track of what rolls over) |

There are two toggles for each card. The eyeball hides the card so that it takes up less screen space. The pencil allows you to do unexpected operations like fixing your load after you've committed it for a job, deleting or adding new gear, changing the names of your contacts, heal without using the project clock, add/remove traumas, etc.

The standard mode for each card will not allow you to change things in unexpected ways; you'll either be limited to the scope of the rules or cards that shouldn't change very often (like the Dossier) will be read-only.

| ![edit-mode-card.png](https://user-images.githubusercontent.com/130337624/230925175-1927544e-79d6-4a52-935e-468431909b28.png) | 
|:--:| 
| The playbook is hidden, the shrewd friends are able to be edited, and the gear is in its standard state. Note the Coin in your loadout! |

Some major benefits of the dynamic character sheet are that it hides abilities you currently can't use and keeps track of rules-based operations for you. For example, the Coin you're carrying is automatically added to your Loadout. This is a rule I completely forgot about until I started building the app!

The Save button floats in the corner, ever reminding you to save when you're done with a set of changes.

Tech nerd stuff:
It's a Progressive Web App using web assembly, written in C# and Blazor as an entirely client app with Ahead of Time compilation (for now). So while it is a bit of a heavy download size for a simple website, it has many benefits like an app. Once you've got it installed, you can turn on airplane mode and it'll still work completely. It's basically a full app that just happens to use the browser to download it once (or when it updates) and to run it.
