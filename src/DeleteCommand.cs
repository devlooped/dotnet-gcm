using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Devlooped;

public class DeleteCommand : AsyncCommand<UrlSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, UrlSettings settings)
    {
        var input = settings.ToInputs();
        if (HostProviders.GetProvider(input) is { } provider)
        {
            await provider.EraseCredentialAsync(input);
            return 0;
        }

        return -1;
    }
}
