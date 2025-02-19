namespace Common.Entities;

public class Truck(
    string brand, 
    string number, 
    int speed, 
    int capacity, 
    bool hasTrailer) : Transport(brand, number, speed, capacity)
{
    public override int Capacity
    {
        get => HasTrailer ? _capacity * 2 : _capacity;
        set => _capacity = value;
    }

    public bool HasTrailer { get; set; } = hasTrailer;

    protected override string GetString()
    {
        return $"Truck(Brand={Brand}, " +
               $"Number={Number}, " +
               $"Speed={Speed}, " +
               $"HasTrailer={HasTrailer}, " +
               $"Capacity={Capacity})";
    }
}
