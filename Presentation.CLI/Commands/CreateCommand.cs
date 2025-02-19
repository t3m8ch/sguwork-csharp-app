using BusinessLogic.Abstract;
using Common.Entities;

namespace Presentation.CLI.Commands;

public class CreateCommand(ITransportService service) : ICommand
{
    public string Verb => "create";
    
    public void Dispatch(string[] pieces)
    {
        Console.Write("Brand: ");
        string? brand = Console.ReadLine();
        if (brand == null || brand.Trim().Length == 0)
        {
            Console.WriteLine("Brand should not be empty");
            return;
        }

        Console.Write("Number: ");
        string? number = Console.ReadLine();
        if (number == null || number.Trim().Length == 0)
        {
            Console.WriteLine("Number should not be empty");
            return;
        }

        Console.Write("Speed: ");
        if (!int.TryParse(Console.ReadLine(), out int speed))
        {
            Console.WriteLine("Speed must be an integer");
            return;
        }

        Console.Write("Capacity: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("Capacity must be an integer");
            return;
        }
        
        Console.WriteLine("Choose transport type:\n" +
                          "1. Passenger car\n" +
                          "2. Motorcycle\n" +
                          "3. Truck");
        if (!int.TryParse(Console.ReadLine(), out int transportTypeNumber))
        {
            Console.WriteLine("Transport type must be an integer");
            return;
        }

        Transport transport;
        switch (transportTypeNumber)
        {
            case 1:
            {
                transport = new PassengerCar(brand, number, speed, capacity);
                break;
            }
            case 2:
            {
                Console.Write("Has sidecar: ");
                if (!bool.TryParse(Console.ReadLine(), out bool hasSidecar))
                {
                    Console.WriteLine("Has sidecar must be true or false");
                }
                
                transport = new Motorcycle(brand, number, speed, capacity, hasSidecar);
                break;
            }
            case 3:
            {
                Console.Write("Has trailer: ");
                if (!bool.TryParse(Console.ReadLine(), out bool hasTrailer))
                {
                    Console.WriteLine("Has trailer must be true or false");
                }

                transport = new Truck(brand, number, speed, capacity, hasTrailer);
                break;
            }
            default:
            {
                Console.WriteLine("Transport type must be 1, 2 or 3");
                return;
            }
        }
        
        service.Add(transport);
    }
}
