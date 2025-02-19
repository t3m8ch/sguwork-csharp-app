namespace Presentation.CLI.Commands;

public class CommandDispatcher(params ICommand[] commands)
{
    public void RunLoop()
    {
        while (true)
        {
            Console.Write("> ");
            string[]? pieces = Console.ReadLine()?.Split(' ');
            if (pieces == null || pieces.Length < 1)
            {
                continue;
            }

            if (pieces[0] == "exit")
            {
                Console.WriteLine("Bye!");
                break;
            }

            bool commandFound = false;
            foreach (var command in commands)
            {
                if (command.Verb == pieces[0])
                {
                    command.Dispatch(pieces);
                    commandFound = true;
                }
            }
            
            if (!commandFound) {
                Console.WriteLine("Unknown command");
            }
        }
    }
}
