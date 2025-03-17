using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Presentation.DesktopUI.Observers;
using Presentation.DesktopUI.ViewModels;

namespace Presentation.DesktopUI.Views;

public partial class AddTransportWindow : Window
{
    public AddTransportWindow()
    {
        InitializeComponent();
        if (DataContext is AddTransportViewModel vm)
        {
            var observer = new AddCommandObserver(this);
            vm.AddCommand.Subscribe(_ => Close());
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}