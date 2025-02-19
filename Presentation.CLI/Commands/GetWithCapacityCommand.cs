using BusinessLogic.Abstract;

namespace Presentation.CLI.Commands;

public class GetWithCapacityCommand(ITransportService service) : ICommand
{
    public string Verb => "getwcap";
    public void Dispatch(string[] pieces)
    {
        if (pieces.Length < 3)
        {
            Console.WriteLine("getwcap (=, >, <, >=, <=) (capacity)");
            return;
        }

        if (!int.TryParse(pieces[2], out int capacity))
        {
            Console.WriteLine("capacity must be an integer");
            return;
        }

        var predicate = GetPredicateForCapacity(pieces[1], capacity);
        if (predicate == null)
        {
            Console.WriteLine("comparision must be =, <, >, <= or >=");
            return;
        }

        var transports = service.GetWithCapacity(predicate);
        transports.ForEach(Console.WriteLine);
    }

    private static Func<int, bool>? GetPredicateForCapacity(string str, int capacity)
    {
        return str switch
        {
            "=" => (c) => c == capacity,
            ">" => (c) => c > capacity,
            "<" => (c) => c < capacity,
            ">=" => (c) => c >= capacity,
            "<=" => (c) => c <= capacity,
            _ => null
        };
    }
}
