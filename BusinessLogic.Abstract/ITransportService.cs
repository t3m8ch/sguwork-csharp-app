using Common.Entities;

namespace BusinessLogic.Abstract;

public interface ITransportService
{
    List<Transport> GetAll();
    List<Transport> GetWithCapacity(Func<int, bool> predicate);
    void Add(Transport transport);
    void Remove(Transport transport);
    void Save();
    List<Transport> AddedTransports { get; }
    List<Transport> RemovedTransports { get; }
    void ResetAddedTransports();
    void ResetRemovedTransports();
}
