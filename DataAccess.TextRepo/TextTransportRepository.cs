using System.Text;
using Common.Entities;
using DataAccess.Abstract;

namespace DataAccess.TextRepo;

public class TextTransportRepository : ITransportRepository
{
    private List<Transport> _transports;
    private readonly string _filePath;

    public TextTransportRepository(string filePath)
    {
        _filePath = filePath;
        _transports = [];
        Read();
    }

    public List<Transport> GetAll()
    {
        return _transports;
    }

    public List<Transport> GetWithCapacity(Func<int, bool> predicate)
    {
        return _transports.Where(t => predicate(t.Capacity)).ToList();
    }

    public void Add(Transport transport)
    {
        _transports.Add(transport);
    }

    public void Save()
    {
        _transports.Sort();
        File.WriteAllLines(_filePath, _transports.Select(GetTransportString));
    }

    private void Read()
    {
        if (!File.Exists(_filePath))
        {
            _transports = [];
            return;
        }

        _transports = File.ReadLines(_filePath).Select(ParseTransportLine).ToList();
    }

    private string GetTransportString(Transport transport)
    {
        var builder = new StringBuilder();
        builder.Append($"{GetTransportTypeStr(transport)}\t" +
                       $"{transport.Brand}\t" +
                       $"{transport.Number}\t" +
                       $"{transport.Speed}\t" +
                       $"{transport.SerializableCapacity}");

        switch (transport)
        {
            case Motorcycle motorcycle:
                builder.Append($"\t{motorcycle.HasSidecar}");
                break;
            case Truck truck:
                builder.Append($"\t{truck.HasTrailer}");
                break;
        }

        return builder.ToString();
    }

    private static string GetTransportTypeStr(Transport transport)
    {
        return transport switch
        {
            PassengerCar => "PassengerCar",
            Motorcycle => "Motorcycle",
            Truck => "Truck",
            _ => "Transport"
        };
    }

    private static Transport ParseTransportLine(string line)
    {
        var pieces = line.Split('\t');
        if (pieces.Length < 5 || 
            (pieces[0] == "Motorcycle" || pieces[0] == "Truck") && pieces.Length < 6)
        {
            throw new InvalidDataException("Invalid line: not enough data");
        }
        
        string brand = pieces[1];
        string number = pieces[2];

        if (!int.TryParse(pieces[3], out int speed))
        {
            throw new InvalidDataException("Invalid line: speed must be a number");
        }

        if (!int.TryParse(pieces[4], out int capacity))
        {
            throw new InvalidDataException("Invalid line: capacity must be a number");
        }

        switch (pieces[0])
        {
            case "PassengerCar":
                return new PassengerCar(brand, number, speed, capacity);
            case "Motorcycle":
                if (!bool.TryParse(pieces[5], out bool hasSidecar))
                {
                    throw new InvalidDataException("Invalid line: hasSidecar must be a boolean");
                }
                
                return new Motorcycle(brand, number, speed, capacity, hasSidecar);
            case "Truck":
                if (!bool.TryParse(pieces[5], out bool hasTrailer))
                {
                    throw new InvalidDataException("Invalid line: hasTrailer must be a boolean");
                }

                return new Truck(brand, number, speed, capacity, hasTrailer);
            default:
                throw new InvalidDataException("Invalid line: invalid transport type");
        }
    }
}
