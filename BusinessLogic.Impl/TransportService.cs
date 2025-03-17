using BusinessLogic.Abstract;
using Common.Entities;
using DataAccess.Abstract;

namespace BusinessLogic.Impl;

public class TransportService(ITransportRepository transportRepository) : ITransportService
{
    private readonly List<Transport> _addedTransports = [];
    private readonly List<Transport> _removedTransports = [];
    
    public List<Transport> GetAll()
    {
        return transportRepository.GetAll();
    }

    public List<Transport> GetWithCapacity(Func<int, bool> predicate)
    {
        return transportRepository.GetWithCapacity(predicate);
    }

    public void Add(Transport transport)
    {
        transportRepository.Add(transport);
        _addedTransports.Add(transport);
    }

    public void Remove(Transport transport)
    {
        transportRepository.Remove(transport);
        _removedTransports.Add(transport);
    }

    public void Save()
    {
        transportRepository.Save();
    }

    public List<Transport> AddedTransports => _addedTransports;
    public List<Transport> RemovedTransports => _removedTransports;
    public void ResetAddedTransports()
    {
        _addedTransports.Clear();
    }

    public void ResetRemovedTransports()
    {
        _removedTransports.Clear();
    }
}
