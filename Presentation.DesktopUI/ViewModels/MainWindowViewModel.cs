﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> FilterCommand { get; }
    public ReactiveCommand<Window, Unit> OpenAddTransportWindowCommand { get; }

    public MainWindowViewModel()
    {
        var repository = new JsonTransportRepository("transports.json");
        _transportService = new TransportService(repository);

        foreach (var transport in _transportService.GetAll())
        {
            Transports.Add(transport);
        }

        SelectedFilterOperator = FilterOperators.First();

        SaveCommand = ReactiveCommand.Create(() => _transportService.Save());
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
            var addWindow = new AddTransportWindow { DataContext = addVm };
            addWindow.ShowDialog(owner);
        });
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
