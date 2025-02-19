namespace Common.Entities;

public class Motorcycle(
    string brand, 
    string number, 
    int speed, 
    int capacity,
    bool hasSidecar)
    : Transport(brand, number, speed, capacity)
{
    public bool HasSidecar { get; set; } = hasSidecar;

    public override int Capacity
    {
        get => HasSidecar ? _capacity : 0;
        set => _capacity = value;
    }

    protected override string GetString()
    {
        return $"Motorcycle(Brand={Brand}, " +
               $"Number={Number}, " +
               $"Speed={Speed}, " +
               $"HasSidecar={HasSidecar}, " +
               $"Capacity={Capacity})";
    }
}
