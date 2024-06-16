extern alias CredentialManager;
using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Devlooped;

[Description("Delete a stored credential.")]
public class DeleteCommand : AsyncCommand<DeleteCommand.DeleteSettings>
{
    public class DeleteSettings : UrlSettings
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

    public override async Task<int> ExecuteAsync(CommandContext context, DeleteSettings settings)
    {
        if (settings.Namespace == null)
        {
            var input = settings.ToInputs();
            if (HostProviders.GetProvider(input) is { } provider)
            {
                await provider.EraseCredentialAsync(input);
                return 0;
            }

            return -1;
        }

        var store = CredentialManager.GitCredentialManager.CredentialManager.Create(settings.Namespace);
        store.Remove(settings.Uri!.AbsoluteUri, settings.Account);
        return 0;
    }
}
