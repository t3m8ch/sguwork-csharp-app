using Newtonsoft.Json;

namespace Common.Entities;

[JsonObject(ItemTypeNameHandling = TypeNameHandling.Auto)]
public abstract class Transport(
    string brand, 
    string number, 
    int speed, 
    int capacity) : IComparable<Transport>
{
    public string Brand { get; set; } = brand;
    public string Number { get; set; } = number;
    public int Speed { get; set; } = speed;

    [JsonProperty("Capacity")]
    protected int _capacity = capacity;
    
    public int SerializableCapacity => _capacity;
    
    [JsonIgnore]
    public virtual int Capacity
    {
        get => _capacity;
        set => _capacity = value;
    }
    protected abstract string GetString();

    public override string ToString()
    {
        return GetString();
    }

    public int CompareTo(Transport? other)
    {
        return other is null ? 1 : Capacity.CompareTo(other.Capacity);
    }
}
