using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Happify.Client
{
    public class MqttService 
    {
        private static MqttService _instance;
        private IMqttClient _mqttClient;

        public static MqttService Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new MqttService();
                return _instance;
            }
        }

        private MqttService()
        {
        }
        
        public async Task<MqttClientConnectResultCode> ConnectAsync()
        {
            MqttFactory factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Create TCP based options using the builder.
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId("Jessica")
                .WithTcpServer("mqtt.ably.io", 8883)
                .WithCredentials("GIn8xA.lwvRbg", "mmkCycAbnYQvmZxS")
                .WithTls()
                .WithCleanSession()
                .Build();

            Debug.Log("Connecting to MQTT...");
            CancellationToken cancellationToken;
            MqttClientAuthenticateResult result = await _mqttClient.ConnectAsync(options, cancellationToken);

            if (result.ResultCode == MqttClientConnectResultCode.Success)
                Debug.Log("Connected successfully to MQTT Broker.");
            else
                Debug.Log($"Failed to connect to MQTT Broken: {result.ReasonString}");

            return result.ResultCode;
        }

        private bool IsConnected() => _mqttClient.IsConnected;

        public async Task PublishAsync(string topic, string payload, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!IsConnected())
            {
                MqttClientConnectResultCode result = await ConnectAsync();
                if (result != MqttClientConnectResultCode.Success)
                    return;
            }

            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

            await _mqttClient.PublishAsync(message, cancellationToken);
        }
    }
}