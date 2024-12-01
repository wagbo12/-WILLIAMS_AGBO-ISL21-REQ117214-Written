/*======================================================================
| MqtHandler class
|
| Name: MqtHandler.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Handles MQTT operations such as connecting, subscribing, and publishing messages.
|
| Usage: Called in the main application to manage MQTT interactions.
|
| Description of properties: None
|
|----
*/

using System;
using System.Text; 
using System.Text.Json; 
using System.Threading; 
using System.Threading.Tasks; 
using MQTTnet; 
using MQTTnet.Client; 

namespace src 
{
    // MqtHandler class handles MQTT operations like connecting, subscribing, publishing, and disconnecting
    public class MqtHandler
    {
        private readonly IMqttClient _mqttClient; // Private field to hold the MQTT client instance

        // Constructor: Initializes the MQTT client using a factory
        public MqtHandler()
        {
            var factory = new MqttFactory(); // Create a new MQTT factory instance
            _mqttClient = factory.CreateMqttClient(); // Create an MQTT client instance
        }

        // Method to connect to the MQTT broker asynchronously
        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder() // Start building MQTT client options
                .WithTcpServer("test.mosquitto.org", 1883) // Set the broker's address and port
                .Build(); // Build the options

            await _mqttClient.ConnectAsync(options, CancellationToken.None); // Connect to the MQTT broker
            Console.WriteLine("Connected to MQTT broker."); // Log a success message
        }

        // Method to subscribe to a topic and handle incoming messages
        public async Task SubscribeAsync(string topic, Action<string> onMessageReceived)
        {
            await _mqttClient.SubscribeAsync(topic); // Subscribe to the specified MQTT topic

            // Set up an event handler to process received messages
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload); // Decode the message payload
                onMessageReceived(message); // Invoke the callback with the message content
            };

            Console.WriteLine($"Subscribed to topic: {topic}"); // Log the subscription
        }

        // Method to publish a message to a specified topic
        public async Task PublishAsync(string topic, string message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder() // Start building the MQTT message
                .WithTopic(topic) // Set the topic for the message
                .WithPayload(message) // Set the message payload
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce) // Set QoS level
                .Build(); // Build the MQTT message

            await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None); // Publish the message
            Console.WriteLine($"Published message to topic: {topic}"); // Log the publication
        }

        // Method to disconnect from the MQTT broker
        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync(); // Disconnect from the broker
            Console.WriteLine("Disconnected from MQTT broker."); // Log the disconnection
        }
    }
}
