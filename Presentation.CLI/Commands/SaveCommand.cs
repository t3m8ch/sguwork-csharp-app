using BusinessLogic.Abstract;

namespace Presentation.CLI.Commands;

public class SaveCommand(ITransportService service) : ICommand
{
    public string Verb => "save";
    public void Dispatch(string[] pieces)
    {
        service.Save();
    }
}
