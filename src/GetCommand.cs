using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Devlooped;

[Description("Get a stored credential.")]
public class GetCommand : AsyncCommand<UrlSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, UrlSettings settings)
    {
        try
        {
            var input = settings.ToInputs();
            var provider = HostProviders.GetProvider(input);

            if (provider != null && await provider.GetCredentialAsync(input) is { } credentials)
            {
                AnsiConsole.MarkupLine($"username: [yellow]{credentials.Account}[/]");
                AnsiConsole.MarkupLine($"password: [yellow]{credentials.Password}[/]");

                return 0;
            }
        }
        catch (InvalidOperationException)
        {
            // throw when no creds found and interactivity is disabled.
            // See AuthenticationBase.ThrowIfUserInteractionDisabled
        }

        return -1;
    }
}
