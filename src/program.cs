/*======================================================================
| Program class
|
| Name: program.cs
|
| Written by: Williams Agbo - 2024-12-01
|
| Purpose: Entry point of the application.
|
| Usage: Initializes services and starts the application.
|
| Description of properties: None
|
|----
*/
using System;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv; 
using src.Models;

namespace src
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Rules Engine Project Running...");

            // Load .env file
            Env.Load();

            // Retrieve credentials from environment variables
            string topicId = Environment.GetEnvironmentVariable("TOPIC_ID") ?? throw new Exception("TOPIC_ID is not set in .env");

            // Define MQTT topics
            string inputTopic = $"BRE/calculateWinterSupplementInput/{topicId}";
            string outputTopic = $"BRE/calculateWinterSupplementOutput/{topicId}";

            // Initialize MqtHandler and RulesEngine
            var mqttHandler = new MqtHandler();
            var rulesEngine = new RulesEngine();

            // Connect to MQTT broker
            await mqttHandler.ConnectAsync();

            // Subscribe to input topic
            await mqttHandler.SubscribeAsync(inputTopic, async (message) =>
            {
                Console.WriteLine($"Received message on {inputTopic}: {message}");

                try
                {
                    // Parse the input data
                    var inputData = JsonSerializer.Deserialize<InputData>(message);

                    // Process data using RulesEngine
                    var outputData = rulesEngine.Process(inputData);

                    // Serialize and publish the result
                    string outputMessage = JsonSerializer.Serialize(outputData);
                    await mqttHandler.PublishAsync(outputTopic, outputMessage);

                    Console.WriteLine($"Published result to {outputTopic}: {outputMessage}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            });

            Console.WriteLine("Listening for MQTT messages. Press Enter to exit...");
            Console.ReadLine();

            // Disconnect from the MQTT broker
            await mqttHandler.DisconnectAsync();
        }
    }
}