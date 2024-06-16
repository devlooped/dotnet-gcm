extern alias CredentialManager;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Devlooped;

[Description("Get a stored credential.")]
public class GetCommand : AsyncCommand<GetCommand.GetSettings>
{
    public class GetSettings : UrlSettings
    {
        [Description("The user or account to retrieve the password for, if using a namespace for generic credentials.")]
        [CommandOption("-u|--username <USERNAME>", IsHidden = true)]
        public string? Account { get; set; }

        public override ValidationResult Validate()
        {
            if (Namespace != null && Account == null)
                return ValidationResult.Error("The user or account is required when using a namespace.");

            return base.Validate();
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetSettings settings)
    {
        if (settings.Namespace == null)
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

        var store = CredentialManager.GitCredentialManager.CredentialManager.Create(settings.Namespace);
        var creds = store.Get(settings.Uri!.AbsoluteUri, settings.Account);
        AnsiConsole.WriteLine(creds.Password);

        return 0;
    }
}
