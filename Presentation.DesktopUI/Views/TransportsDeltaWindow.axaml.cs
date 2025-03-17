using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Presentation.DesktopUI.ViewModels;

namespace Presentation.DesktopUI.Views;

public partial class TransportsDeltaWindow : Window
{
    public TransportsDeltaWindow(TransportsDeltaViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}