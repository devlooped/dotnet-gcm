![Icon](https://raw.githubusercontent.com/devlooped/dotnet-gcm/main/icon.png) dotnet-gcm
============

[![Version](https://img.shields.io/nuget/v/dotnet-gcm.svg?color=royalblue)](https://www.nuget.org/packages/dotnet-gcm) [![Downloads](https://img.shields.io/nuget/dt/dotnet-gcm.svg?color=green)](https://www.nuget.org/packages/dotnet-gcm) [![License](https://img.shields.io/github/license/devlooped/dotnet-gcm.svg?color=blue)](https://github.com//devlooped/dotnet-gcm/blob/main/license.txt) [![Build](https://github.com/devlooped/dotnet-gcm/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/dotnet-gcm/actions)

<!-- #content -->
A dotnet global tool for managing Git credentials using the cross-platform Git Credential Manager (GCM) 
for Windows, macOS and Linux, with support for Azure DevOps, Bitbucket, GitHub, and GitLab.

```
USAGE:
    dotnet gcm [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    get       Get a stored credential
    delete    Delete a stored credential
    set       Store a credential
```

Note that all commands can operate on a simplified syntax using a full URI, which can include `username:password` 
(as in the `set` command). That argument is converted to a `Uri` and the existing options are used as the default 
value for required options that aren't provided. You can alternatively provide the individual options.


### dotnet gcm get

```
DESCRIPTION:
Get a stored credential.

USAGE:
    dotnet gcm get [URL] [OPTIONS]

ARGUMENTS:
    [URL]    A URL used to populate options from a single value: [protocol]://[user]:[password]@[host]/[path?]

OPTIONS:
    -h, --help               Prints help information
    -s, --scheme <SCHEME>    The protocol over which the credential will be used (e.g., https)
    -h, --host <HOST>        The remote hostname for a network credential. This can include the port number
        --path <PATH>        The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's
                             path on the server
```

### dotnet gcm set

```
DESCRIPTION:
Store a credential.

USAGE:
    dotnet gcm set [URL] [OPTIONS]

ARGUMENTS:
    [URL]    A URL used to populate options from a single value: [protocol]://[user]:[password]@[host]/[path?]

OPTIONS:
    -h, --help                   Prints help information
    -s, --scheme <SCHEME>        The protocol over which the credential will be used (e.g., https)
    -h, --host <HOST>            The remote hostname for a network credential. This can include the port number
        --path <PATH>            The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the
                                 repository's path on the server
    -u, --username <USERNAME>    The credential's username
    -p, --password <PASSWORD>    The credential's password
```

### dotnet gcm delete

```
DESCRIPTION:
Delete a stored credential.

USAGE:
    dotnet gcm delete [URL] [OPTIONS]

ARGUMENTS:
    [URL]    A URL used to populate options from a single value: [protocol]://[user]:[password]@[host]/[path?]

OPTIONS:
    -h, --help               Prints help information
    -s, --scheme <SCHEME>    The protocol over which the credential will be used (e.g., https)
    -h, --host <HOST>        The remote hostname for a network credential. This can include the port number
        --path <PATH>        The path with which the credential will be used. E.g., for accessing a remote https repository, this will be the repository's
                             path on the server
```

<!-- #content -->
<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![Torutek](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/torutek-gh.png "Torutek")](https://github.com/torutek-gh)
[![DRIVE.NET, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/drivenet.png "DRIVE.NET, Inc.")](https://github.com/drivenet)
[![Keith Pickford](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Keflon.png "Keith Pickford")](https://github.com/Keflon)
[![Thomas Bolon](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tbolon.png "Thomas Bolon")](https://github.com/tbolon)
[![Kori Francis](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/kfrancis.png "Kori Francis")](https://github.com/kfrancis)
[![Toni Wenzel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/twenzel.png "Toni Wenzel")](https://github.com/twenzel)
[![Uno Platform](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/unoplatform.png "Uno Platform")](https://github.com/unoplatform)
[![Dan Siegel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/dansiegel.png "Dan Siegel")](https://github.com/dansiegel)
[![Reuben Swartz](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/rbnswartz.png "Reuben Swartz")](https://github.com/rbnswartz)
[![Jacob Foshee](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jfoshee.png "Jacob Foshee")](https://github.com/jfoshee)
[![](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Mrxx99.png "")](https://github.com/Mrxx99)
[![Eric Johnson](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/eajhnsn1.png "Eric Johnson")](https://github.com/eajhnsn1)
[![Ix Technologies B.V.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/IxTechnologies.png "Ix Technologies B.V.")](https://github.com/IxTechnologies)
[![David JENNI](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/davidjenni.png "David JENNI")](https://github.com/davidjenni)
[![Jonathan ](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Jonathan-Hickey.png "Jonathan ")](https://github.com/Jonathan-Hickey)
[![Charley Wu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/akunzai.png "Charley Wu")](https://github.com/akunzai)
[![Jakob Tikjøb Andersen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jakobt.png "Jakob Tikjøb Andersen")](https://github.com/jakobt)
[![Tino Hager](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tinohager.png "Tino Hager")](https://github.com/tinohager)
[![Ken Bonny](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KenBonny.png "Ken Bonny")](https://github.com/KenBonny)
[![Simon Cropp](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/SimonCropp.png "Simon Cropp")](https://github.com/SimonCropp)
[![agileworks-eu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agileworks-eu.png "agileworks-eu")](https://github.com/agileworks-eu)
[![sorahex](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sorahex.png "sorahex")](https://github.com/sorahex)
[![Zheyu Shen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/arsdragonfly.png "Zheyu Shen")](https://github.com/arsdragonfly)
[![Vezel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/vezel-dev.png "Vezel")](https://github.com/vezel-dev)
[![ChilliCream](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/ChilliCream.png "ChilliCream")](https://github.com/ChilliCream)
[![4OTC](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/4OTC.png "4OTC")](https://github.com/4OTC)
[![Vincent Limo](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/v-limo.png "Vincent Limo")](https://github.com/v-limo)
[![Jordan S. Jones](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jordansjones.png "Jordan S. Jones")](https://github.com/jordansjones)
[![domischell](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/DominicSchell.png "domischell")](https://github.com/DominicSchell)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
