using DataAccess.Abstract;
using DataAccess.TextRepo;

namespace Presentation.CLI.FileChoosing;

public class TextFileType : IFileType
{
    public string Title => "Text";
    public string Extension => "txt";

    public Func<string, ITransportRepository> CreateTransportRepository =>
        filePath => new TextTransportRepository(filePath);
}
