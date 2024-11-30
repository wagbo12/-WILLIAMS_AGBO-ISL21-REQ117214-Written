using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace src
{
    public class MqtHandler
    {
        private readonly IMqttClient _mqttClient;

        public MqtHandler()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
        }

        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("test.mosquitto.org", 1883) 
                .Build();

            await _mqttClient.ConnectAsync(options, CancellationToken.None);
            Console.WriteLine("Connected to MQTT broker.");
        }

        public async Task SubscribeAsync(string topic, Action<string> onMessageReceived)
        {
            await _mqttClient.SubscribeAsync(topic);

            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                onMessageReceived(message);
            };

            Console.WriteLine($"Subscribed to topic: {topic}");
        }

        public async Task PublishAsync(string topic, string message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce) 
                .Build();

            await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
            Console.WriteLine($"Published message to topic: {topic}");
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
            Console.WriteLine("Disconnected from MQTT broker.");
        }
    }
}