using System;
using System.Linq;
using Atlassian.Bitbucket;
using GitCredentialManager;
using GitHub;
using GitLab;
using Microsoft.AzureRepos;
using Spectre.Console;

namespace Devlooped;

public static class HostProviders
{
    static IHostProvider[] providers;

    static HostProviders()
    {

        var context = new CommandContext();
        providers =
        [
            new GitHubHostProvider(context),
            new GitLabHostProvider(context),
            new BitbucketHostProvider(context),
            new AzureReposHostProvider(context),
        ];
    }

    public static IHostProvider? GetProvider(InputArguments input)
    {
        var provider = providers.FirstOrDefault(x => x.IsSupported(input));

        if (provider == null)
        {
            AnsiConsole.MarkupLine("[red]An authentication provider supporting the given inputs was not found.[/]");
            AnsiConsole.MarkupLine("Supported hosts are:");

            foreach (var host in providers)
            {
                AnsiConsole.MarkupLine(" - " + host.Name);
            }
        }

        return provider;
    }
}
