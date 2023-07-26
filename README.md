Hi all!

I made a character sheet web app for Blades in the Dark. It now also supports Scum and Villainy! Moving forward, supporting more Forged in the Dark games will be much easier.

<a href='https://ko-fi.com/S6S5KA4DP' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi3.png?v=3' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>

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

### Supporting more Forged in the Dark games

I rewrote the app to load a game-settings.json file dynamically. You can find the schema for that here: UI/wwwroot/data/game-settings-schema.json

If you're willing to help convert the game text into a json file, I'm typically willing to support the game in the app.

### Release Notes

#### Version 9

- Create a game-settings-schema.json file that can be found here: UI/wwwroot/data/game-settings-schema.json

- Migrate all the hardcoded Blades in the Dark stuff to a gamesettings file.

- Support [Scum and Villainy](https://evilhat.com/product/scum-and-villainy/) with the help of redditor u/djsteele888 who filled out the vast majority of the Scum and Villainy json file.

- Support all the dynamic loading of game settings within the code.

- Remove the automatic assignment of coin to bulk in favor of coin items that can be added when necessary.

- Support dll trimming for hopefully a smaller payload. (JITerpreter stuff is coming out in .NET 8 at the end of the year and I'd like to see if that behaves better than AOT for most people.)

- If you have any trouble with the app, loading your characters, or importing your character json files, please seek me out on reddit, the blades in the dark discord, or file an issue here!

### Version 10

- Working on increasing accessibility

- Colors have somewhat changed for improved contrast

- Aria-labels are being implemented more thoughtfully

- Text used in place of icons more often (screenreaders seem to handle that better)

- Identified some problem areas that still need work: Expansion Panels, "Ratings" (exp trackers)

#### Version 10.1

- More screen reader fixes

- Cutter friend's list fix

- Veteran says 99 times now for BitD (not S&V)--adding custom abilities and custom friends to come in future update

#### Version 10.2

- Support for adding and removing friends from the friends list. (It's a bit awkward due to limited space, you need to upgrade to close friend or downgrade to rival first to expose the button. This should be discoverable though because people start with one of each type of friend.)