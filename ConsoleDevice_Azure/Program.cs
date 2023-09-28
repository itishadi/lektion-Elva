using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

DeviceClient deviceClient = DeviceClient
.CreateFromConnectionString("HostName=KYH-MyIoT-hubb.azure-devices.net;DeviceId=SimulateIotPhoneCall;SharedAccessKey=uXv+v+qvNbYELJskT9v9xNDD56nu0hEuPfLzzUlqNWU=",
TransportType.Mqtt);

var data = new
{
    Message = "Mitt Meddelande",
    Created = DateTime.Now
};
await SendingTelemetryAsync(JsonConvert.SerializeObject(data));
async Task SendingTelemetryAsync(string payloadAsJson)
{
    while (true)
    {
        var message = new Message(Encoding.UTF8.GetBytes(payloadAsJson));
        await deviceClient.SendEventAsync(message);

        Console.WriteLine($"Message sent at {DateTime.Now} with payload {payloadAsJson}");
        await Task.Delay(5000);
    }
}