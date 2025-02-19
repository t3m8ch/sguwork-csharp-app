using Common.Entities;

namespace BusinessLogic.Abstract;

public interface ITransportService
{
    List<Transport> GetAll();
    List<Transport> GetWithCapacity(Func<int, bool> predicate);
    void Add(Transport transport);
    void Save();
}
