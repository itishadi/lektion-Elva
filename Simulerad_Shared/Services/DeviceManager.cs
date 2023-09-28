using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using Simulerad_Iot.Models.Devices;
using Simulerad_Shared.Models.Devices;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Simulerad_Shared.Services
{
    public class DeviceManager
    {
        private readonly DeviceConfiguration _config;
        private readonly DeviceClient _client;

        public DeviceManager(DeviceConfiguration config)
        {
            _config = config;
            _client = DeviceClient.CreateFromConnectionString(_config.ConnectionString);

            // Lägg till Device Twin-hanterare separat utan att påverka Direct Method-hanteraren.
            Task.FromResult(StartDirectMethodHandlerAsync());
            Task.FromResult(StartDeviceTwinHandlerAsync());
        }

        public bool AllowSending() => _config.AllowSending;

        private async Task StartDirectMethodHandlerAsync()
        {
            await _client.SetMethodDefaultHandlerAsync(DirectMethodDefaultCallback, null);
        }

        private async Task StartDeviceTwinHandlerAsync()
        {
            await _client.SetDesiredPropertyUpdateCallbackAsync(DesiredPropertyUpdateCallback, null);
        }

        private async Task<MethodResponse> DirectMethodDefaultCallback(MethodRequest req, object userContext)
        {
            var res = new DirectMethodDataResponse { Message = $"Executed method: {req.Name} successfully." };

            bool updateTwin = false; // En flagga för att bestämma om Device Twin ska uppdateras

            switch (req.Name.ToLower())
            {
                case "start":
                    _config.AllowSending = true;
                    updateTwin = true; // Sätt flaggan till true om metoden är "start"
                    break;
                case "stop":
                    _config.AllowSending = false;
                    updateTwin = true; // Sätt flaggan till true om metoden är "stop"
                    break;
                default:
                    res.Message = $"Direct method: {req.Name} not found.";
                    return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 400);
            }

            if (updateTwin)
            {
                // Uppdatera Device Twin-egenskapen baserat på _config.AllowSending
                await DeviceTwinManager.UpdateReportedTwinAsync(_client, "AllowSending", _config.AllowSending);
            }

            return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 200);
        }


        // Device Twin hanterare för att hantera önskade egenskaper.
        private Task DesiredPropertyUpdateCallback(TwinCollection desiredProperties, object userContext)
        {
            if (desiredProperties.Contains("AllowSending"))
            {
                _config.AllowSending = desiredProperties["AllowSending"];
                Console.WriteLine($"Device Twin: Updated AllowSending to {_config.AllowSending}");
            }

            return Task.CompletedTask;
        }
    }
}

