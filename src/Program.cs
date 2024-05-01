using System;
using System.Collections.Generic;
using System.Linq;
using Devlooped;
using Spectre.Console.Cli;

// We never run interactively, since this is mostly a tool for automation/CI environments
Environment.SetEnvironmentVariable("GCM_INTERACTIVE", "never", EnvironmentVariableTarget.Process);
Environment.SetEnvironmentVariable("GIT_TERMINAL_PROMPT", "false", EnvironmentVariableTarget.Process);

var app = new CommandApp();
app.Configure(config =>
{
    // Change so it matches the actual user experience as a dotnet CLI extension
    config.SetApplicationName("dotnet gcm");

    config.Settings.HelpProviderStyles = new Spectre.Console.Cli.Help.HelpProviderStyle
    {
        Description = new Spectre.Console.Cli.Help.DescriptionStyle
        {
            Header = new Spectre.Console.Style(Spectre.Console.Color.Yellow, decoration: Spectre.Console.Decoration.Bold),
        },
        Usage = new Spectre.Console.Cli.Help.UsageStyle
        {
            Header = new Spectre.Console.Style(Spectre.Console.Color.Yellow, decoration: Spectre.Console.Decoration.Bold),
            Command = new Spectre.Console.Style(Spectre.Console.Color.Green, decoration: Spectre.Console.Decoration.Bold),
            CurrentCommand = new Spectre.Console.Style(Spectre.Console.Color.Green, decoration: Spectre.Console.Decoration.Bold),
            OptionalArgument = new Spectre.Console.Style(Spectre.Console.Color.Grey),
            RequiredArgument = new Spectre.Console.Style(Spectre.Console.Color.White, decoration: Spectre.Console.Decoration.Bold),
            Options = new Spectre.Console.Style(Spectre.Console.Color.Blue, decoration: Spectre.Console.Decoration.Bold),
        },
        Arguments = new Spectre.Console.Cli.Help.ArgumentStyle
        {
            Header = new Spectre.Console.Style(Spectre.Console.Color.Yellow, decoration: Spectre.Console.Decoration.Bold),
            OptionalArgument = new Spectre.Console.Style(Spectre.Console.Color.Grey),
            RequiredArgument = new Spectre.Console.Style(Spectre.Console.Color.White, decoration: Spectre.Console.Decoration.Bold),
        },
        Options = new Spectre.Console.Cli.Help.OptionStyle
        {
            Header = new Spectre.Console.Style(Spectre.Console.Color.Yellow, decoration: Spectre.Console.Decoration.Bold),
            OptionalOption = new Spectre.Console.Style(Spectre.Console.Color.Grey),
            RequiredOption = new Spectre.Console.Style(Spectre.Console.Color.Blue, decoration: Spectre.Console.Decoration.Bold),
        },
        Commands = new Spectre.Console.Cli.Help.CommandStyle
        {
            Header = new Spectre.Console.Style(Spectre.Console.Color.Yellow, decoration: Spectre.Console.Decoration.Bold),
            RequiredArgument = new Spectre.Console.Style(Spectre.Console.Color.Blue, decoration: Spectre.Console.Decoration.Bold),
        },
    };

    config.AddCommand<GetCommand>("get");
    config.AddCommand<DeleteCommand>("delete");
    config.AddCommand<SetCommand>("set");
});

// For backwards compatibility, replace old command names.
if (args.Length > 0 && args[0] == "erase")
    args[0] = "delete";
if (args.Length > 0 && args[0] == "store")
    args[0] = "set";

// Replace --protocol > --scheme
await app.RunAsync(args.Select(x => x.StartsWith("--protocol", StringComparison.OrdinalIgnoreCase) ? x.Replace("--protocol", "--scheme") : x));