![Icon](https://raw.githubusercontent.com/devlooped/dotnet-gcm/main/icon.png) dotnet-gcm
============

[![Version](https://img.shields.io/nuget/v/dotnet-gcm.svg?color=royalblue)](https://www.nuget.org/packages/dotnet-gcm) [![Downloads](https://img.shields.io/nuget/dt/dotnet-gcm.svg?color=green)](https://www.nuget.org/packages/dotnet-gcm) [![License](https://img.shields.io/github/license/devlooped/dotnet-gcm.svg?color=blue)](https://github.com//devlooped/dotnet-gcm/blob/main/license.txt) [![Build](https://github.com/devlooped/dotnet-gcm/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/dotnet-gcm/actions)

<!-- #content -->
A dotnet global tool for managing Git credentials using the Microsoft Git Credentials Manager Core.

```
Usage:
  gcm [options] [command]

Options:
  --version         Show version information
  -?, -h, --help    Show help and usage information

Commands:
  get <url>     Get a stored credential.
  set <url>     Store a credential.
  delete <url>  Delete a stored credential.
```

Note that all commands can operate on a simplified syntax using a full URI, which can include `username:password` 
(as in the `set` command). That argument is converted to a `Uri` and the existing options are used as the default 
value for required options that aren't provided. You can alternatively provide the individual options.


**get**: Get a stored credential.

```
Usage:
  gcm [options] get [<url>]

Arguments:
  <url>  A URL used to populate options from a single value: [protocol]://[host]/[path?]

Options:
  -p, --protocol <protocol> (REQUIRED)  The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)          The remote hostname for a network credential. This can include the port number.
  --path <path>                         The path with which the credential will be used. E.g., for accessing a remote
                                        https repository, this will be the repository's path on the server.
  -?, -h, --help                        Show help and usage information
```

**set**: Store a credential.

```
Usage:
  gcm [options] set [<url>]

Arguments:
  <url>  A URL used to populate options from a single value: [protocol]://[user]:[password]@[host]/[path?]

Options:
  -s, --protocol <protocol> (REQUIRED)  The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)          The remote hostname for a network credential. This can include the port number.
  -u, --username <username> (REQUIRED)  The credential's username.
  -p, --password <password> (REQUIRED)  The credential's password.
  --path <path>                         The path with which the credential will be used. E.g., for accessing a remote https repository, this
                                        will be the repository's path on the server.
  -?, -h, --help                        Show help and usage information
```

**delete**: Delete a stored credential.

```
Usage:
  gcm [options] delete [<url>]

Arguments:
  <url>  A URL used to populate options from a single value: [protocol]://[host]/[path?]

Options:
  -p, --protocol <protocol> (REQUIRED)  The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)          The remote hostname for a network credential. This can include the port number.
  --path <path>                         The path with which the credential will be used. E.g., for accessing a remote https repository, this
                                        will be the repository's path on the server.
  -?, -h, --help                        Show help and usage information
```

<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![Christian Findlay](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MelbourneDeveloper.png "Christian Findlay")](https://github.com/MelbourneDeveloper)
[![C. Augusto Proiete](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/augustoproiete.png "C. Augusto Proiete")](https://github.com/augustoproiete)
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![SandRock](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sandrock.png "SandRock")](https://github.com/sandrock)
[![Eric C](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/eeseewy.png "Eric C")](https://github.com/eeseewy)
[![Andy Gocke](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agocke.png "Andy Gocke")](https://github.com/agocke)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
