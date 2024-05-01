using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Devlooped;

[Description("Store a credential.")]
public class SetCommand : AsyncCommand<CredentialUrlSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, CredentialUrlSettings settings)
    {
        var input = settings.ToInputs();

        if (HostProviders.GetProvider(input) is { } provider)
        {
            await provider.StoreCredentialAsync(input);
            return 0;
        }

        return -1;
    }
}
