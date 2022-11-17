using System;
using System.IO;
using System.Reflection;

namespace DotNet;

static class ApplicationBase
{
    public static string GetEntryApplicationPath()
    {
#if NETFRAMEWORK
            // Single file publishing does not exist with .NET Framework so
            // we can just use reflection to get the entry assembly path.
            return Assembly.GetEntryAssembly().Location;
#else
        // Assembly::Location always returns an empty string if the application
        // was published as a single file
#pragma warning disable IL3000
        bool isSingleFile = string.IsNullOrEmpty(Assembly.GetEntryAssembly()?.Location);
#pragma warning restore IL3000

        // Use "argv[0]" to get the full path to the entry executable in
        // .NET 5+ when published as a single file.
        string[] args = Environment.GetCommandLineArgs();
        string candidatePath = args[0];

        // If we have not been published as a single file then we must strip the
        // ".dll" file extension to get the default AppHost/SuperHost name.
        if (!isSingleFile && Path.HasExtension(candidatePath))
        {
            return Path.ChangeExtension(candidatePath, null);
        }

        return candidatePath;
#endif
    }
}
