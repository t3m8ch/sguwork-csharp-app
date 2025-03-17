using System.Reactive;
using ReactiveUI;

namespace Presentation.DesktopUI.ViewModels;

public class ConfirmDeleteViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, bool> ConfirmCommand { get; }
    public ReactiveCommand<Unit, bool> CancelCommand { get; }

    public ConfirmDeleteViewModel()
    {
        ConfirmCommand = ReactiveCommand.Create(() => true);
        CancelCommand = ReactiveCommand.Create(() => false);
    }
}