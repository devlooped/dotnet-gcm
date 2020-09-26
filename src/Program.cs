using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Atlassian.Bitbucket;
using GitHub;
using Microsoft.AzureRepos;
using Microsoft.Git.CredentialManager;

namespace gcm
{
    class Program
    {
        static readonly IHostProvider[] providers;

        static Program()
        {
            // We never run interactively, since this is mostly a tool for automation/CI environments
            Environment.SetEnvironmentVariable("GCM_INTERACTIVE", "never", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("GIT_TERMINAL_PROMPT", "false", EnvironmentVariableTarget.Process);

            var context = new CommandContext();
            providers = new IHostProvider[]
            {
                new GitHubHostProvider(context),
                new BitbucketHostProvider(context),
                new AzureReposHostProvider(context),
            };
        }

        static async Task Main(string[] args)
        {
            var store = new Command("store", "Store a credential.")
            {
                new Option<string>(new string[] { "--protocol", "-p" }, "The protocol over which the credential will be used (e.g., https).")
                {
                    IsRequired = true
                },
                new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                {
                    IsRequired = true
                },
                new Option<string>(new string[] { "--username", "-usr" }, "The credential's username.")
                {
                    IsRequired = true
                },
                new Option<string>(new string[] { "--password", "-pwd" }, "The credential's password.")
                {
                    IsRequired = true
                },
                new Option<string?>(new string[] { "--path", }, "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
            };
            store.Handler = CommandHandler.Create<string, string, string, string, string>(StoreAsync);

            var erase = new Command("erase", "Erase a stored credential.")
            {
                new Option<string>(new string[] { "--protocol", "-p" }, "The protocol over which the credential will be used (e.g., https).")
                {
                    IsRequired = true
                },
                new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                {
                    IsRequired = true
                },
                new Option<string?>(new string[] { "--path", }, "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
            };
            erase.Handler = CommandHandler.Create<string, string, string?>(EraseAsync);

            var get = new Command("get", "Get a stored credential.")
            {
                new Option<string>(new string[] { "--protocol", "-p" }, "The protocol over which the credential will be used (e.g., https).")
                {
                    IsRequired = true
                },
                new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                {
                    IsRequired = true
                },
                new Option<string?>(new string[] { "--path", }, "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
            };
            get.Handler = CommandHandler.Create<string, string, string?>(GetAsync);

            await new RootCommand
            {
                erase,
                get,
                store
            }.InvokeAsync(args);
        }

        static async Task EraseAsync(string protocol, string host, string? path)
        {
            var arguments = new Dictionary<string, string>
            {
                [nameof(protocol)] = protocol,
                [nameof(host)] = host,
            };

            if (!string.IsNullOrEmpty(path))
                arguments[nameof(path)] = path;

            var input = new InputArguments(arguments);
            var provider = GetProvider(input);
            if (provider != null)
                await provider.EraseCredentialAsync(input);
        }

        static async Task GetAsync(string protocol, string host, string? path)
        {
            var arguments = new Dictionary<string, string>
            {
                [nameof(protocol)] = protocol,
                [nameof(host)] = host,
            };

            if (!string.IsNullOrEmpty(path))
                arguments[nameof(path)] = path;

            try
            {
                var input = new InputArguments(arguments);
                var provider = GetProvider(input);
                if (provider != null)
                {
                    var credentials = await provider.GetCredentialAsync(input);
                    Console.WriteLine("username: " + credentials.Account);
                    Console.WriteLine("password: " + credentials.Password);
                }
            }
            catch (InvalidOperationException)
            {
                // throw when no creds found and interactivity is disabled.
                // See AuthenticationBase.ThrowIfUserInteractionDisabled
            }
        }

        static async Task StoreAsync(string protocol, string host, string username, string password, string? path)
        {
            var arguments = new Dictionary<string, string>
            {
                [nameof(protocol)] = protocol,
                [nameof(host)] = host,
                [nameof(username)] = username,
                [nameof(password)] = password
            };

            if (!string.IsNullOrEmpty(path))
                arguments[nameof(path)] = path;

            var input = new InputArguments(arguments);
            var provider = GetProvider(input);
            if (provider != null)
                await provider.StoreCredentialAsync(input);
        }

        static IHostProvider? GetProvider(InputArguments input)
        {
            var provider = providers.FirstOrDefault(x => x.IsSupported(input));

            if (provider == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An authentication provider supporting the given inputs was not found. Supported domains are: *.github.com, *.bitbucket.org, *.visualstudio.com and dev.azure.com.");
                Console.ResetColor();
                Environment.ExitCode = -1;
            }

            return provider;
        }
    }
}
