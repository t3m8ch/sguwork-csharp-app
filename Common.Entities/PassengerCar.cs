namespace Common.Entities;

public class PassengerCar(
    string brand, 
    string number, 
    int speed, 
    int capacity) : Transport(brand, number, speed, capacity)
{
    protected override string GetString()
    {
        return $"PassengerCar(Brand={Brand}, " +
               $"Number={Number}, " +
               $"Speed={Speed}, " +
               $"Capacity={Capacity})";
    }
}
