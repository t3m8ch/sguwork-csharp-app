using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Presentation.DesktopUI.ViewModels;

namespace Presentation.DesktopUI.Views;

public partial class ConfirmDeleteWindow : Window
{
    public ConfirmDeleteWindow(ConfirmDeleteViewModel confirmVm)
    {
        InitializeComponent();
        DataContext = confirmVm;
        
        confirmVm.ConfirmCommand.Subscribe(result => Close(result));
        confirmVm.CancelCommand.Subscribe(result => Close(result));
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}