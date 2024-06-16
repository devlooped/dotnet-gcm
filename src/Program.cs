using System;
using System.Linq;
using Devlooped;
using Spectre.Console;
using Spectre.Console.Cli;

// We never run interactively, since this is mostly a tool for automation/CI environments
Environment.SetEnvironmentVariable("GCM_INTERACTIVE", "never", EnvironmentVariableTarget.Process);
Environment.SetEnvironmentVariable("GIT_TERMINAL_PROMPT", "false", EnvironmentVariableTarget.Process);


if (args.Contains("--version"))
{
    AnsiConsole.MarkupLine($"{ThisAssembly.Project.ToolCommandName} version [lime]{ThisAssembly.Project.Version}[/] ({ThisAssembly.Project.BuildDate})");
    AnsiConsole.MarkupLine($"[link]{ThisAssembly.Git.Url}/releases/tag/{ThisAssembly.Project.BuildRef}[/]");
    return;
}

if (args.Contains("-?"))
    args = args.Select(x => x == "-?" ? "--help" : x).ToArray();
if (args.Contains("-h"))
    args = args.Select(x => x == "-h" ? "--help" : x).ToArray();

var app = new CommandApp();
app.Configure(config =>
{
    // Change so it matches the actual user experience as a dotnet CLI extension
    config.SetApplicationName("dotnet gcm");
    config.PrettyHelper();

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