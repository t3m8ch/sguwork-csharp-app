using BusinessLogic.Abstract;

namespace Presentation.CLI.Commands;

public class GetAllCommand(ITransportService service) : ICommand
{
    public string Verb => "getall";
    public void Dispatch(string[] pieces)
    {
        var transports = service.GetAll();
        transports.ForEach(Console.WriteLine);
    }
}
