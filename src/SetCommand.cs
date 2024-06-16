extern alias CredentialManager;
using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Devlooped;

[Description("Store a credential.")]
public class SetCommand : AsyncCommand<CredentialUrlSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, CredentialUrlSettings settings)
    {
        if (settings.Namespace == null)
        {
            var input = settings.ToInputs();

            if (HostProviders.GetProvider(input) is { } provider)
            {
                await provider.StoreCredentialAsync(input);
                return 0;
            }

            return -1;
        }

        var store = CredentialManager.GitCredentialManager.CredentialManager.Create(settings.Namespace);
        // settings validation ensures we always have a URI
        store.AddOrUpdate(settings.Uri!.AbsoluteUri, settings.Username, settings.Password);
        return 0;
    }
}
