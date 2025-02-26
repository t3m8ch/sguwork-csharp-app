using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using BusinessLogic.Abstract;
using BusinessLogic.Impl;
using Common.Entities;
using DataAccess.JsonRepo;
using ReactiveUI;

namespace Presentation.DesktopUI.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    private readonly ITransportService _transportService;
    public ObservableCollection<Transport> Transports { get; } = [];
    public List<string> TransportTypes { get; } = ["PassengerCar", "Truck", "Motorcycle"];
    
    private string _selectedTransportType;
    public string SelectedTransportType
    {
        get => _selectedTransportType;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedTransportType, value);
            this.RaisePropertyChanged(nameof(IsMotorcycle));
            this.RaisePropertyChanged(nameof(IsTruck));
        }
    }
    
    public bool IsMotorcycle => SelectedTransportType == "Motorcycle";
    public bool IsTruck => SelectedTransportType == "Truck";
    
    private string _brand;
    public string Brand
    {
        get => _brand;
        set => this.RaiseAndSetIfChanged(ref _brand, value);
    }

    private string _number;
    public string Number
    {
        get => _number;
        set => this.RaiseAndSetIfChanged(ref _number, value);
    }

    private int _speed;
    public int Speed
    {
        get => _speed;
        set => this.RaiseAndSetIfChanged(ref _speed, value);
    }

    private int _capacity;
    public int Capacity
    {
        get => _capacity;
        set => this.RaiseAndSetIfChanged(ref _capacity, value);
    }

    private bool _hasSidecar;
    public bool HasSidecar
    {
        get => _hasSidecar;
        set => this.RaiseAndSetIfChanged(ref _hasSidecar, value);
    }
    
    private bool _hasTrailer;
    public bool HasTrailer
    {
        get => _hasTrailer;
        set => this.RaiseAndSetIfChanged(ref _hasTrailer, value);
    }
    
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
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> FilterCommand { get; }

    public MainWindowViewModel()
    {
        var repository = new JsonTransportRepository("transports.json");
        _transportService = new TransportService(repository);
        
        foreach (var transport in _transportService.GetAll())
        {
            Transports.Add(transport);
        }

        SelectedTransportType = TransportTypes.First();
        SelectedFilterOperator = FilterOperators.First();

        AddCommand = ReactiveCommand.Create(AddTransport);
        SaveCommand = ReactiveCommand.Create(() => _transportService.Save());
        FilterCommand = ReactiveCommand.Create(FilterTransports);
    }
    
    private void AddTransport()
    {
        Transport? newTransport = SelectedTransportType switch
        {
            "PassengerCar" => new PassengerCar(Brand, Number, Speed, Capacity),
            "Truck"        => new Truck(Brand, Number, Speed, Capacity, HasTrailer),
            "Motorcycle"   => new Motorcycle(Brand, Number, Speed, Capacity, HasSidecar),
            _ => null
        };

        if (newTransport == null) return;
        
        _transportService.Add(newTransport);
        Transports.Add(newTransport);
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
}
