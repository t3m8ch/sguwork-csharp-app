using BusinessLogic.Abstract;

namespace Presentation.CLI.Commands;

public class RemoveCommand(ITransportService service) : ICommand
{
    public string Verb => "remove";
    public void Dispatch(string[] pieces)
    {
        var transports = service.GetAll();
        
        Console.WriteLine("Выберите номер транспорта для удаления:");
        for (int i = 1; i <= transports.Count; i++) 
        {
            Console.WriteLine($"{i}. {transports[i - 1]}");
        }
        
        Console.Write("> ");

        if (!int.TryParse(Console.ReadLine(), out int transportNumber))
        {
            Console.WriteLine("Вы должны были ввести число");
            return;
        }

        if (transportNumber < 1 || transportNumber > transports.Count)
        {
            Console.WriteLine("Вы должны выбрать номер транспорта из списка");
            return;
        }

        var transportToRemove = transports[transportNumber - 1];
        
        Console.Write($"Вы действительно хотите удалить этот транспорт [да/нет]?\n" +
                      $"{transportToRemove}\n" +
                      $"> ");
        
        var answer = Console.ReadLine();
        if (answer?.ToLower().Trim() == "да")
        {
            service.Remove(transportToRemove);
            return;
        }
        
        Console.WriteLine("Действие отменено");
    }
}