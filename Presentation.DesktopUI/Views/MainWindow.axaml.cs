using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Presentation.DesktopUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var button = this.FindControl<Button>("AddTransportButton");
        if (button != null)
        {
            button.CommandParameter = this;
        }
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
