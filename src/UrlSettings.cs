using System;
using System.Collections.Generic;
using System.ComponentModel;
using GitCredentialManager;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Devlooped;

public static class UrlSettingsExtensions
{
    public static InputArguments ToInputs(this UrlSettings settings)
    {
        var arguments = new Dictionary<string, string>
        {
            ["protocol"] = settings.Scheme!,
            ["host"] = settings.Host!,
        };

        if (!string.IsNullOrEmpty(settings.Path))
            arguments["path"] = settings.Path;

        return new InputArguments(arguments);
    }

    public static InputArguments ToInputs(this CredentialUrlSettings settings)
    {
        var arguments = new Dictionary<string, string>
        {
            ["protocol"] = settings.Scheme!,
            ["host"] = settings.Host!,
            ["username"] = settings.Username!,
            ["password"] = settings.Password!,
        };

        if (!string.IsNullOrEmpty(settings.Path))
            arguments["path"] = settings.Path;

        return new InputArguments(arguments);
    }
}

public class UrlSettings : CommandSettings
{
    [Description("A URL used to populate options from a single value: [[protocol]]://[[user]]:[[password]]@[[host]]/[[path?]]")]
    [CommandArgument(0, "[URL]")]
    public virtual Uri? Uri { get; set; }

    [Description("The protocol over which the credential will be used (e.g., https).")]
    [CommandOption("-s|--scheme <SCHEME>")]
    public string? Scheme { get; set; }

    [Description("The remote hostname for a network credential. This can include the port number.")]
    [CommandOption("-h|--host <HOST>")]
    public string? Host { get; set; }

    [Description("The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the server.")]
    [CommandOption("--path <PATH>")]
    public string? Path { get; set; }

    public override ValidationResult Validate()
    {
        if (Uri != null)
        {
            Scheme = Uri.Scheme;
            Host = Uri.Host;
            Path = Uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
        }

        return base.Validate();
    }
}

public class CredentialUrlSettings : UrlSettings
{
    [Description("The credential's username.")]
    [CommandOption("-u|--username <USERNAME>")]
    public string? Username { get; set; }

    [Description("The credential's password.")]
    [CommandOption("-p|--password <PASSWORD>")]
    public string? Password { get; set; }

    public override ValidationResult Validate()
    {
        if (Uri != null && Uri.TryGetUserInfo(out var user, out var password))
        {
            Username = user;
            Password = password;
        }

        return base.Validate();
    }
}
