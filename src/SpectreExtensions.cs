using Spectre.Console;
using Spectre.Console.Cli;

namespace Devlooped;

static class SpectreExtensions
{
    public static IConfigurator PrettyHelper(this IConfigurator config)
    {
        config.Settings.HelpProviderStyles = new Spectre.Console.Cli.Help.HelpProviderStyle
        {
            Description = new Spectre.Console.Cli.Help.DescriptionStyle
            {
                Header = new Style(Color.Yellow, decoration: Decoration.Bold),
            },
            Usage = new Spectre.Console.Cli.Help.UsageStyle
            {
                Header = new Style(Color.Yellow, decoration: Decoration.Bold),
                Command = new Style(Color.Green, decoration: Decoration.Bold),
                CurrentCommand = new Style(Color.Green, decoration: Decoration.Bold),
                OptionalArgument = new Style(Color.Grey),
                RequiredArgument = new Style(Color.White, decoration: Decoration.Bold),
                Options = new Style(Color.Blue, decoration: Decoration.Bold),
            },
            Arguments = new Spectre.Console.Cli.Help.ArgumentStyle
            {
                Header = new Style(Color.Yellow, decoration: Decoration.Bold),
                OptionalArgument = new Style(Color.Grey),
                RequiredArgument = new Style(Color.White, decoration: Decoration.Bold),
            },
            Options = new Spectre.Console.Cli.Help.OptionStyle
            {
                Header = new Style(Color.Yellow, decoration: Decoration.Bold),
                OptionalOption = new Style(Color.Grey),
                RequiredOption = new Style(Color.Blue, decoration: Decoration.Bold),
            },
            Commands = new Spectre.Console.Cli.Help.CommandStyle
            {
                Header = new Style(Color.Yellow, decoration: Decoration.Bold),
                RequiredArgument = new Style(Color.Blue, decoration: Decoration.Bold),
            },
        };

        return config;
    }
}
