using Common.Entities;

namespace DataAccess.Abstract;

public interface ITransportRepository
{
    List<Transport> GetAll();
    List<Transport> GetWithCapacity(Func<int, bool> predicate);
    void Add(Transport transport);
    void Save();
}
