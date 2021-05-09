using System.CommandLine.Invocation;
using System.Reflection;
using System.Threading.Tasks;

namespace System.CommandLine
{
    static class CommandExtensions
    {
        public static T WithHandler<T>(this T command, string name) where T : Command
            => (T)WithHandler<T>((Command)command, name);

        public static Command WithHandler<T>(this Command command, string name)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
            var method = typeof(T).GetMethod(name, flags) ??
                throw new ArgumentException($"Counld not find method {typeof(T).Name}.{name}.", nameof(name));

            if (method.IsStatic)
                command.Handler = new CancellableHandler(CommandHandler.Create(method));
            else
                command.Handler = new CancellableHandler(CommandHandler.Create(method, command));

            return command;
        }

        class CancellableHandler : ICommandHandler
        {
            readonly ICommandHandler handler;

            public CancellableHandler(ICommandHandler handler) => this.handler = handler;

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                try
                {
                    return await handler.InvokeAsync(context);
                }
                catch (Exception e) when (e is TaskCanceledException || e is OperationCanceledException)
                {
                    return 0;
                }
            }
        }
    }
}
