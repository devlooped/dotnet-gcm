![Icon](https://raw.githubusercontent.com/kzu/dotnet-gcm/main/icon.png) dotnet-gcm
============

[![Version](https://img.shields.io/nuget/v/dotnet-gcm.svg?color=royalblue)](https://www.nuget.org/packages/dotnet-gcm)
[![Downloads](https://img.shields.io/nuget/dt/dotnet-gcm.svg?color=green)](https://www.nuget.org/packages/dotnet-gcm)
[![License](https://img.shields.io/github/license/kzu/dotnet-gcm.svg?color=blue)](https://github.com//kzu/dotnet-gcm/blob/main/LICENSE)
[![Build](https://github.com/kzu/dotnet-gcm/workflows/build/badge.svg?branch=main)](https://github.com/kzu/dotnet-gcm/actions)

A dotnet global tool for managing Git credentials using the Microsoft Git Credentials Manager Core.

```
Usage:
  gcm [options] [command]

Options:
  --version         Show version information
  -?, -h, --help    Show help and usage information

Commands:
  erase    Erase a stored credential.
  get      Get a stored credential.
  store    Store a credential.
```

**erase**: Erase a stored credential.

```
Usage:
  gcm erase [options]

Options:
  -p, --protocol <protocol> (REQUIRED)    The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)            The remote hostname for a network credential. This can include the port number.
  --path <path>                           The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the
                                          server.
```

**get**: Get a stored credential.

```
Usage:
  gcm get [options]

Options:
  -p, --protocol <protocol> (REQUIRED)    The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)            The remote hostname for a network credential. This can include the port number.
  --path <path>                           The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the
                                          server.
```

**store**: Store a credential.

```
Usage:
  gcm store [options]

Options:
  -p, --protocol <protocol> (REQUIRED)      The protocol over which the credential will be used (e.g., https).
  -h, --host <host> (REQUIRED)              The remote hostname for a network credential. This can include the port number.
  -usr, --username <username> (REQUIRED)    The credential's username.
  -pwd, --password <password> (REQUIRED)    The credential's password.
  --path <path>                             The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's path on the
                                            server.
```
