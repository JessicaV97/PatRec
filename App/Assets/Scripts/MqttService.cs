using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Happify.Client
{
    /// <summary>
    /// Class used to send messages from app to chairable. Used in the study environment and quiz. 
    /// </summary>
    public class MqttService 
    {
        // Create instance of MQTT service
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
        
        /// <summary>
        /// Create connection between client and broker
        /// </summary>
        /// <returns></returns>
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

        //Check if the connection between broker and client is existent. 
        private bool IsConnected() => _mqttClient.IsConnected;

        /// <summary>
        /// Class to send the messages to the client
        /// </summary>
        /// <param name="topic"></param>
        /// Refers to the channel that is used to send/receive messages.
        /// <param name="payload"></param>
        /// Refers to the message that is being sent. 
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

            Debug.Log(payload);
            await _mqttClient.PublishAsync(message, cancellationToken);
        }
    }
}