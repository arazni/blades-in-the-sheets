# How to run locally

To build the application locally, you'll need [the dotnet9 software development kit](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

I recommend using git to clone the repository in a directory of your choice from a terminal application like command prompt or powershell. These are often available in sections of [visual studio code](https://code.visualstudio.com/download) as well, if you don't already have a preferred code editor.

1. From the terminal, in the folder that has the solution file (`.sln`), you should be able to run `dotnet build`.

2. Verify that everything builds correctly (no red errors).
    - You may get an error informing you to download a specific tool. The error should tell you what command to run. It will probably be something like: `dotnet workload restore` or `dotnet workload install wasm-tools`. You may need to run it with administrative privileges, so you may need to open your terminal by running as admin and then do this step.

3. Change directory into the UI folder and run `dotnet watch`, which should automatically open a browser when it's ready at a localhost address. `dotnet watch --project UI/UI.csproj` will work as well if you want to stay in the same folder.

## Tips for testing locally with `dotnet watch`

Some changes will "hot reload", where changing the source files will be reflected immediately on the webpage. I think how inconsistent hot reload is may be the most disappointing/frustrating feature of Blazor.

I think JSON changes will at least require you to refresh the page, but will probably not require the app to rebuild.

If a rebuild is required, the terminal that's running `dotnet watch` should prompt you to let it restart when you make a change it can't automatically update without rebuilding. You can set it to always do so with `a`, which will cause it to rebuild automatically for this instance of using `dotnet watch`.

If it's behaving weirdly, using `Ctrl+C` to kill it and then doing `dotnet watch` again will often improve the behavior. Sometimes restarting your PC will improve it as well.

## Some general developer guidance

The app must work with [NVDA screen readers](https://www.nvaccess.org/download/). This is why we're using the component library we're using, which unfortunately wraps around JS components that modify the DOM and the Shadow DOM. It is a necessary headache as every other Blazor library I've tested is unacceptable on this issue. (Though if I knew I'd end up having to rewrite this app with a component library that wraps around JS anyway, I wouldn't have used Blazor in the first place). Custom components will be subject to scrutiny to make sure they work with this screen reader tool. Prioritize accessibility for blind folks over visual niceties.

As much UI code as possible should be done in Blazor, but JS interop is possible when required.

A component usually has a .razor file (for markup and layout), a .razor.cs file (for the majority of the code logic), and sometimes a razor.css file. CSS will by default only be available to the component creating the file and not its child components (such as any Fluent component). To make it apply, you have to use the `::deep` prefix.

When reasonable, prefer for game logic to be done in the Model layer, then the Persistence layer, as these are unit testable. That said, some pieces of logic are clearly tied to the component itself and not really tied to the game.
