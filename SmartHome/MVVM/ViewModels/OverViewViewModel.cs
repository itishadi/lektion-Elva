using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SmartHome.MVVM.ViewModels;

public partial class OverViewViewModel : ObservableObject
{
    //database
    //private readonly IotHubManager _iotHubManager;

    //[ObservableProperty]
    //ObservableCollection<DeviceItemViewModel> devicesList;

    //public OverViewViewModel(IotHubManager iotHubManager)
    //{
    //    _iotHubManager = iotHubManager;
    //    _iotHubManager.InitializeAsync().ConfigureAwait(true);

    //    UpdateDeviceList();
    //    _iotHubManager.DeviceItemListUpdated += UpdateDeviceList;
    //}

    //private void UpdateDeviceList()
    //{
    //    DevicesList = new ObservableCollection<DeviceItemViewModel>(_iotHubManager.DeviceItemList
    //        .Select(deviceItem => new DeviceItemViewModel(deviceItem)).ToList());
    //}

}
