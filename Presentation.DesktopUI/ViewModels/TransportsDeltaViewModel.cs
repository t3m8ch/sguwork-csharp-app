using System.Collections.Generic;
using Common.Entities;

namespace Presentation.DesktopUI.ViewModels;

public class TransportsDeltaViewModel(List<Transport> addedTransports, List<Transport> removedTransports)
{
    public List<Transport> AddedTransports { get; set; } = addedTransports;
    public List<Transport> RemovedTransports { get; set; } = removedTransports;
}
