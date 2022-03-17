using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Simulation.Events;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simulation.Proxies
{
    public class MqttTrafficControlService : ITrafficControlService
    {
        //private readonly IMqttClient _client;
        private readonly IMqttClient _client;


        public MqttTrafficControlService(int camNumber)
        {
            // connect to mqtt broker
            var mqttHost = Environment.GetEnvironmentVariable("MQTT_HOST") ?? "localhost";
            //_client = MqttClient.CreateAsync(mqttHost, 1883).ConfigureAwait(false).GetAwaiter().GetResult();
            //var sessionState = _client.ConnectAsync(
            //    new MqttClientCredentials(clientId: $"camerasim{camNumber}")).ConfigureAwait(false).GetAwaiter().GetResult();
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId($"camerasim{camNumber}")
                .WithTcpServer(mqttHost,6009)
                .Build();

            _client.ConnectAsync(options).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        public async Task SendVehicleEntryAsync(VehicleRegistered vehicleRegistered)
        {
            var eventJson = JsonSerializer.Serialize(vehicleRegistered);
            await _client.PublishAsync("trafficcontrol/entrycam", eventJson, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce);

        }

        public async Task SendVehicleExitAsync(VehicleRegistered vehicleRegistered)
        {
            var eventJson = JsonSerializer.Serialize(vehicleRegistered);
            await _client.PublishAsync("trafficcontrol/exitcam", eventJson, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce);
        }
    }
}