using System.Collections.Generic;
using System.Reactive;
using BusinessLogic.Abstract;
using Common.Entities;
using ReactiveUI;

namespace Presentation.DesktopUI.ViewModels;

public class AddTransportViewModel : ReactiveObject
{
    private readonly ITransportService _transportService;
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

    public ReactiveCommand<Unit, Transport?> AddCommand { get; }

    public AddTransportViewModel(ITransportService transportService)
    {
        _transportService = transportService;
        SelectedTransportType = TransportTypes[0];
        AddCommand = ReactiveCommand.Create(() =>
        {
            Transport? newTransport = SelectedTransportType switch
            {
                "PassengerCar" => new PassengerCar(Brand, Number, Speed, Capacity),
                "Truck" => new Truck(Brand, Number, Speed, Capacity, HasTrailer),
                "Motorcycle" => new Motorcycle(Brand, Number, Speed, Capacity, HasSidecar),
                _ => null
            };
            if (newTransport != null)
            {
                _transportService.Add(newTransport);
                return newTransport;
            }
            return null;
        });
    }
}