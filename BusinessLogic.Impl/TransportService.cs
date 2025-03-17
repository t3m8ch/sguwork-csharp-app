using BusinessLogic.Abstract;
using Common.Entities;
using DataAccess.Abstract;

namespace BusinessLogic.Impl;

public class TransportService(ITransportRepository transportRepository) : ITransportService
{
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
    }

    public void Remove(Transport transport)
    {
        transportRepository.Remove(transport);
    }

    public void Save()
    {
        transportRepository.Save();
    }
}
