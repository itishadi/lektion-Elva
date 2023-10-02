using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartHome.MVVM.Models;

namespace SmartHome.MVVM.ViewModels;

public partial class AllDevicesViewModel : ObservableObject
{



    [RelayCommand]
    async Task GoBack() => await Shell.Current.GoToAsync("..");


    //
    private DeviceItem _deviceItem;

    public AllDevicesViewModel(DeviceItem deviceItem)
    {
        _deviceItem = deviceItem;
        Location = deviceItem.Location ?? "";
        IsActive = deviceItem.IsActive;
        Icon = SetDeviceIcon();
    }

    public string DeviceId => _deviceItem.DeviceId ?? "";
    public string DeviceType => _deviceItem.DeviceType ?? "";
    public string Vendor => _deviceItem.Vendor ?? "";

    [ObservableProperty]
    string location;


    [ObservableProperty]
    bool isActive;

    [ObservableProperty]
    string icon;


    private string SetDeviceIcon()
    {
        return DeviceType.ToLower() switch
        {
            "Light" => "\uf2db",
            _ => "\uf2db",
        };
    }
    //

}