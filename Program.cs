using Contexto.CLI;

namespace Contexto
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = args.Length > 0 ? args[0] : "run";
            new CommandRouter().Execute(command);
        }
    }
}
