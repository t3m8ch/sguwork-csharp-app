using Common.Entities;
using DataAccess.Abstract;
using Newtonsoft.Json;

namespace DataAccess.JsonRepo;

public class JsonTransportRepository : ITransportRepository
{
    private readonly JsonSerializerSettings _jsonSettings;
    private readonly string _filePath;
    private List<Transport> _transports;
    
    public JsonTransportRepository(string filePath)
    {
        _jsonSettings = new JsonSerializerSettings
        { 
            TypeNameHandling = TypeNameHandling.Auto, 
            Formatting = Formatting.Indented
        };
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
        
        string json = JsonConvert.SerializeObject(_transports, _jsonSettings);
        using var writer = new StreamWriter(_filePath);
        writer.Write(json);
    }

    private void Read()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }        
        
        string json = File.ReadAllText(_filePath);
        _transports = JsonConvert.DeserializeObject<List<Transport>>(json, _jsonSettings) ?? 
                      throw new InvalidOperationException();
    }
}
