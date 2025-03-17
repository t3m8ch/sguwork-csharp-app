using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using BusinessLogic.Abstract;
using BusinessLogic.Impl;
using Common.Entities;
using DataAccess.JsonRepo;
using Presentation.DesktopUI.Views;
using ReactiveUI;

namespace Presentation.DesktopUI.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    private readonly ITransportService _transportService;
    public ObservableCollection<Transport> Transports { get; } = [];

    public List<string> FilterOperators { get; } = [">", ">=", "<", "<=", "="];

    private string _selectedFilterOperator;
    public string SelectedFilterOperator
    {
        get => _selectedFilterOperator;
        set => this.RaiseAndSetIfChanged(ref _selectedFilterOperator, value);
    }

    private int _filterCapacity;
    public int FilterCapacity
    {
        get => _filterCapacity;
        set => this.RaiseAndSetIfChanged(ref _filterCapacity, value);
    }
    
    private Transport? _selectedTransport;
    public Transport? SelectedTransport
    {
        get => _selectedTransport;
        set => this.RaiseAndSetIfChanged(ref _selectedTransport, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> FilterCommand { get; }
    public ReactiveCommand<Window, Unit> OpenAddTransportWindowCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }

    public MainWindowViewModel()
    {
        var repository = new JsonTransportRepository("transports.json");
        _transportService = new TransportService(repository);

        foreach (var transport in _transportService.GetAll())
        {
            Transports.Add(transport);
        }

        SelectedFilterOperator = FilterOperators.First();

        SaveCommand = ReactiveCommand.Create(() =>
        {
            _transportService.Save();
            var deltaViewModel = new TransportsDeltaViewModel(
                _transportService.AddedTransports, 
                _transportService.RemovedTransports
            );
            var deltaWindow = new TransportsDeltaWindow(deltaViewModel);
            deltaWindow.ShowDialog(App.MainWindow);
            
            _transportService.ResetAddedTransports();
            _transportService.ResetRemovedTransports();
        });
        FilterCommand = ReactiveCommand.Create(FilterTransports);
        OpenAddTransportWindowCommand = ReactiveCommand.Create<Window>(owner =>
        {
            var addVm = new AddTransportViewModel(_transportService);
            addVm.AddCommand.Subscribe(newTransport =>
            {
                if (newTransport != null)
                {
                    Transports.Add(newTransport);
                }
            });
            var addWindow = new AddTransportWindow(addVm);
            addWindow.ShowDialog(owner);
        });
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedTransport, 
            this.WhenAnyValue(x => x.SelectedTransport).Select(x => x != null));
    }

    private void FilterTransports()
    {
        Func<int, bool> predicate = SelectedFilterOperator switch
        {
            ">"  => cap => cap >  FilterCapacity,
            ">=" => cap => cap >= FilterCapacity,
            "<"  => cap => cap <  FilterCapacity,
            "<=" => cap => cap <= FilterCapacity,
            "="  => cap => cap == FilterCapacity,
            _    => _      => true
        };

        var filtered = _transportService.GetWithCapacity(predicate);

        Transports.Clear();
        foreach (var transport in filtered)
        {
            Transports.Add(transport);
        }
    }
    
    private async void DeleteSelectedTransport()
    {
        if (SelectedTransport == null) return;
        
        var confirmVm = new ConfirmDeleteViewModel();
        var confirmWindow = new ConfirmDeleteWindow(confirmVm);
        
        var result = await confirmWindow.ShowDialog<bool>(App.MainWindow);
        if (!result) return;

        _transportService.Remove(SelectedTransport);
        Transports.Remove(SelectedTransport);
        SelectedTransport = null;
    }
}
