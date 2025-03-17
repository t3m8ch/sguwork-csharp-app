using System;
using Common.Entities;

namespace Presentation.DesktopUI.Observers;

public class AddCommandObserver(Avalonia.Controls.Window window) : IObserver<Transport?>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Transport? value)
    {
        throw new NotImplementedException();
    }
}