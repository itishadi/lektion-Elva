using System.Diagnostics;
using SmartHome.MVVM.Models;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;

namespace SmartHome.Services;

public class DeviceManager
{
    private readonly string _connectionString = "HostName=KYH-MyIoT-hubb.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=nFRmciFKiUeZb9ZpZ5k3mlknTW4bQ9pR9AIoTMXr/x4=";
    private readonly RegistryManager _registryManager;
    private readonly ServiceClient _serviceClient;
    private readonly System.Timers.Timer _timer;


    public List<DeviceItem> Devices { get; private set; }
    public event Action DevicesUpdated;

    public DeviceManager()
    {
        _registryManager = RegistryManager.CreateFromConnectionString(_connectionString);
        _serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);

        Devices = new List<DeviceItem>();

        _timer = new System.Timers.Timer(5000);
        _timer.Elapsed += async (s, e) => await GetAllDevicesAsync();
        _timer.Start();
    }

    private async Task GetAllDevicesAsync()
    {
        try
        {
            var updated = false;
            var list = new List<Twin>();
            var result = _registryManager.CreateQuery("select * from devices");

            foreach (var item in await result.GetNextAsTwinAsync())
                list.Add(item);


            foreach (var device in list)
                if (!Devices.Any(x => x.DeviceId == device.DeviceId))
                {
                    var _device = new DeviceItem { DeviceId = device.DeviceId };

                    try
                    {
                        _device.DeviceType = device.Properties.Reported["deviceType"].ToString();
                    }
                    catch { }
                    try
                    {
                        _device.Vendor = device.Properties.Reported["vendor"].ToString();

                    }
                    catch { }
                    try
                    {
                        _device.Location = device.Properties.Reported["location"].ToString();

                    }
                    catch { }
                    try
                    {
                        _device.IsActive = bool.Parse(!string.IsNullOrEmpty(device.Properties.Reported["isActive"].ToString()));

                    }
                    catch { }

                    Devices.Add(_device);
                    updated = true;
                }
            for (int i = Devices.Count - 1; i >= 0; i--)
            {
                if (!list.Any(x => x.DeviceId == Devices[i].DeviceId))
                {
                    Devices.RemoveAt(i);
                    updated = true;
                }
            }

            if (updated)
            {
                DevicesUpdated.Invoke();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
    }
    public async Task SendDirectMethodAsync(string deviceId, string methodName)
    {
        var method = new CloudToDeviceMethod(methodName);
        await _serviceClient.InvokeDeviceMethodAsync(deviceId, method);
    }
}



