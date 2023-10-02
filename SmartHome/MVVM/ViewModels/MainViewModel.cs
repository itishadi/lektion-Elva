using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using SmartHome.MVVM.Pages;
using SmartHome.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmartHome.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [RelayCommand]
    async Task GoToSettings() => await Shell.Current.GoToAsync(nameof(SettingsPage));

    [RelayCommand]
    async Task GoToAllDevices() => await Shell.Current.GoToAsync(nameof(AllDevicesPage));

    [RelayCommand]
    void ToggleState(object obj)
    {
        // send direct method message to deviceId
    }




    public async Task CheckConfiguration()
    {
        if (!IsConfigured)
            await Shell.Current.GoToAsync(nameof(GetStartedPage));
    }





    //
    private readonly DeviceManager _deviceManager;
    private readonly DataContext _context;

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

    public MainViewModel(DeviceManager deviceManager, DataContext context)
    {
        _deviceManager = deviceManager;
        _context = context;

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

    private async Task AddConnectionStringAsync(string connectionString)
    {
        _context.Settings.Add(new DataAccess.Entities.SettingsEntity { ConnectionString = connectionString });
        await _context.SaveChangesAsync();
    }

    private async Task<string> GetConnectionStringAsync()
    {
        var result = await _context.Settings.FirstOrDefaultAsync();
        if (result != null)
            return result.ConnectionString;

        return null!;
    }    //


}

