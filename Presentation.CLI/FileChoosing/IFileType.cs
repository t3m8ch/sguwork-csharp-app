using DataAccess.Abstract;

namespace Presentation.CLI.FileChoosing;

public interface IFileType
{
    string Title { get; }
    string Extension { get; }
    Func<string, ITransportRepository> CreateTransportRepository { get; }
}
