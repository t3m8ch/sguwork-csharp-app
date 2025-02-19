namespace Presentation.CLI.Commands;

public interface ICommand
{
    string Verb { get; }
    void Dispatch(string[] pieces);
}
