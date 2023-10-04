using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using SmartApp.MVVM.Views;
using SmartHome.MVVM.Pages;
using SmartHome.MVVM.Views;
using SmartHome.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmartHome.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{
    //public DataTimeViewModel DataTimeViewModel { get; } = new DataTimeViewModel();


    [RelayCommand]
    async Task GoToSettings() => await Shell.Current.GoToAsync(nameof(SettingsPage));

    [RelayCommand]
    async Task GoToAllDevices() => await Shell.Current.GoToAsync(nameof(AllDevicesPage));

    [RelayCommand]
    async Task GoToDateTime() => await Shell.Current.GoToAsync(nameof(DataTimePage));

    [RelayCommand]
    void ToggleState(object obj)
    {
        // send direct method message to deviceId
    }




  


    //
    private readonly DeviceManager _deviceManager;

    [RelayCommand]
    async static Task GoBack() => await Shell.Current.GoToAsync("..");


    [ObservableProperty]
    bool isConfigured;

    [ObservableProperty]
    ObservableCollection<AllDevicesViewModel> devices;

    [RelayCommand]
    public async void SendDirectMethod(AllDevicesViewModel deviceItem)
    {
        try
        {
            var methodName = string.Empty;
            if (!deviceItem.IsActive)
            {
                deviceItem.IsActive = true;
                methodName = "start";
            }
            else
            {
                deviceItem.IsActive = false;
                methodName = "stop";
            }

            await _deviceManager.SendDirectMethodAsync(deviceItem.DeviceId, methodName);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); deviceItem.IsActive = false; }
    }

    public MainViewModel(DeviceManager deviceManager )
    {
        _deviceManager = deviceManager;

        IsConfigured = false;

        //        var result = Task.FromResult(GetConnectionStringAsync()).Result;
        //        var connectionstring = result.Result;
        //        if (connectionstring != null)
        IsConfigured = true;

        if (!IsConfigured)
        {
            //Task.Run(() => Shell.Current.GoToAsync(nameof(GetStartedPage)));
            //            Task.Run(() => AddConnectionStringAsync("HostName=kyh-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=M/vLVpxoLM7Blwqdsc8YxXaW2A7rQRLjzAIoTFa78jI="));

            IsConfigured = true;
        }



        Devices = new ObservableCollection<AllDevicesViewModel>(_deviceManager.Devices
            .Select(device => new AllDevicesViewModel(device)).ToList());

        _deviceManager.DevicesUpdated += UpdateDeviceList;



    }

    private void UpdateDeviceList()
    {
        Devices = new ObservableCollection<AllDevicesViewModel>(_deviceManager.Devices
            .Select(device => new AllDevicesViewModel(device)).ToList());
    }
    //database
    //private readonly ZeniAppDbContext _context;
    //private readonly IotHubManager _iotHubManager;

    //public MainViewModel(ZeniAppDbContext context, IotHubManager iotHubManager)
    //{
    //    _context = context;
    //    _iotHubManager = iotHubManager;
    //    CheckConfigurationAsync().ConfigureAwait(false);

    //}

    //private async Task CheckConfigurationAsync()
    //{
    //    try
    //    {
    //        if (await _context.Settings.AnyAsync())
    //        {
    //            await _iotHubManager.InitializeAsync();
    //            await Shell.Current.GoToAsync(nameof(OverviewPage));
    //        }

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //}


    [RelayCommand]
    async Task GoToGetStarted()
    {
        await Shell.Current.GoToAsync(nameof(GetStartedPage));
    }

}

