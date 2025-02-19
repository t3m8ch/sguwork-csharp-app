using DataAccess.Abstract;
using DataAccess.JsonRepo;

namespace Presentation.CLI.FileChoosing;

public class JsonFileType : IFileType
{
    public string Title => "JSON";
    public string Extension => "json";

    public Func<string, ITransportRepository> CreateTransportRepository =>
        filePath => new JsonTransportRepository(filePath);
}
