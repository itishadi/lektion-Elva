using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;

namespace Simulerad_Shared.Services;

public class DeviceTwinManager
{
    public static async Task<object> GetDeviceTwinPropertyAsync(DeviceClient deviceClient, string key)
    {
        try
        {

            var twin = await deviceClient.GetTwinAsync();
            if (twin.Properties.Desired.Contains(key))
            {
                return twin.Properties.Desired[key];
            }

        }
        catch (Exception ex) { Console.WriteLine($"Unable to update Reported Twin. Error: {ex.Message}"); }
        return null!;
    }
    public static async Task UpdateReportedTwinAsync(DeviceClient deviceClient, string key, object value)
    {
        try
        {

            var twinProperties = new TwinCollection();
            twinProperties[key] = value;
            await deviceClient.UpdateReportedPropertiesAsync(twinProperties);
            Console.WriteLine($"Repoted Twin Property: {key} updated with value: {value}");

        }
        catch (Exception ex) { Console.WriteLine($"Unable to update Reported Twin. Error: {ex.Message}"); }
    }
}
