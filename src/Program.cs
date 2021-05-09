using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Reflection;
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

            var context = new CommandContext(GetApplicationPath());
            providers = new IHostProvider[]
            {
                new GitHubHostProvider(context),
                new BitbucketHostProvider(context),
                new AzureReposHostProvider(context),
            };
        }

        // See GCM's Program.cs
        static string GetApplicationPath()
        {
            // Assembly::Location always returns an empty string if the application was published as a single file
#pragma warning disable IL3000
            bool isSingleFile = string.IsNullOrEmpty(Assembly.GetEntryAssembly()?.Location);
#pragma warning restore IL3000

            // Use "argv[0]" to get the full path to the entry executable - this is consistent across
            // .NET Framework and .NET >= 5 when published as a single file.
            string[] args = Environment.GetCommandLineArgs();
            string candidatePath = args[0];

            // If we have not been published as a single file on .NET 5 then we must strip the ".dll" file extension
            // to get the default AppHost/SuperHost name.
            if (!isSingleFile && Path.HasExtension(candidatePath))
            {
                return Path.ChangeExtension(candidatePath, null);
            }

            return candidatePath;
        }

        static async Task Main(string[] args)
        {
            var root = new RootCommand
            {
                new Command("get", "Get a stored credential.")
                {
                    new Argument<Uri?>("url", "A URL used to populate options from a single value: [protocol]://[host]/[path?]")
                    {
                        Arity = ArgumentArity.ZeroOrOne
                    },
                    new Option<string>(new string[] { "--protocol", "-p" }, "The protocol over which the credential will be used (e.g., https).")
                    {
                        IsRequired = true
                    },
                    new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                    {
                        IsRequired = true
                    },
                    new Option<string?>("--path", "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
                }
                .WithHandler<Program>(nameof(GetAsync)),
                new Command("set", "Store a credential.")
                {
                    new Argument<string?>("url",
                        new ParseArgument<string>(result => {
                            //var r = result.FindResultFor(result. "url");
                            return "";
                        }),
                    description: "A URL used to populate options from a single value: [protocol]://[user]:[password]@[host]/[path?]")
                    {
                        Arity = ArgumentArity.ZeroOrOne,
                    },
                    new Option<string>(new string[] { "--protocol", "-s" },
                        description: "The protocol over which the credential will be used (e.g., https).")
                    {
                        IsRequired = true,
                    },
                    new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                    {
                        IsRequired = true
                    },
                    new Option<string>(new string[] { "--username", "-u" }, "The credential's username.")
                    {
                        IsRequired = true
                    },
                    new Option<string>(new string[] { "--password", "-p" }, "The credential's password.")
                    {
                        IsRequired = true
                    },
                    new Option<string?>("--path", "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
                }
                .WithHandler<Program>(nameof(StoreAsync)),
                new Command("delete", "Delete a stored credential.")
                {
                    new Argument<string?>("url", "A URL used to populate options from a single value: [protocol]://[host]/[path?]")
                    {
                        Arity = ArgumentArity.ZeroOrOne
                    },
                    new Option<string>(new string[] { "--protocol", "-p" }, "The protocol over which the credential will be used (e.g., https).")
                    {
                        IsRequired = true
                    },
                    new Option<string>(new string[] { "--host", "-h" }, "The remote hostname for a network credential. This can include the port number.")
                    {
                        IsRequired = true
                    },
                    new Option<string?>("--path", "The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server."),
                }
                .WithHandler<Program>(nameof(EraseAsync))
            };

            // For backwards compatibility, replace old command names.
            if (args.Length > 0 && args[0] == "erase")
                args[0] = "delete";
            if (args.Length > 0 && args[0] == "store")
                args[0] = "set";

            await new CommandLineBuilder(root)
               .UseMiddleware(async (context, next) =>
                {
                    var arg = context.ParseResult.CommandResult.Children.GetByAlias("url");
                    if (arg != null)
                    {
                        if (!Uri.TryCreate(arg.Tokens[0].Value, UriKind.Absolute, out var uri))
                            throw new ArgumentException($"Invalid URI {arg.Tokens[0].Value}");
                        else
                        {
                            var newArgs = new List<string>(args)
                            {
                                "--protocol",
                                uri.Scheme,
                                "--host",
                                uri.Host,
                            };
                            if (uri.TryGetUserInfo(out var user, out var password))
                            {
                                newArgs.Add("--username");
                                newArgs.Add(user);
                                newArgs.Add("--password");
                                newArgs.Add(password);
                            }
                            var path = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
                            if (!string.IsNullOrEmpty(path))
                            {
                                newArgs.Add("--path");
                                newArgs.Add(path);
                            }

                            context.ParseResult = context.Parser.Parse(newArgs);
                            await next(context);
                            return;
                        }
                    }
                    await next(context);
                })
                .UseDefaults()
                .Build()
                .InvokeAsync(args);
        }

        static async Task EraseAsync(string protocol, string host, string? path)
        {
            var arguments = new Dictionary<string, string>
            {
                [nameof(protocol)] = protocol,
                [nameof(host)] = host,
            };

            if (!string.IsNullOrEmpty(path))
                arguments[nameof(path)] = path!;

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
                arguments[nameof(path)] = path!;

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
                arguments[nameof(path)] = path!;

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
