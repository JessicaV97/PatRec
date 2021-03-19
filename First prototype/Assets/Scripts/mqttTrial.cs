using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MQTTnet;
//using MQTTnet.Client.Options;
//using MQTTnet.Server;
//using MQTTnet.Client.Receiving;
//using Windows.UI.Core;

public class mqttTrial : MonoBehaviour
{
    //private async void SendMQTT()
    //{
    //    // Create a new MQTT client.
    //    var factory = new MqttFactory();
    //    var mqttClient = factory.CreateMqttClient();

    //    // Create TCP based options using the builder.
    //    var options = new MqttClientOptionsBuilder()
    //        .WithClientId("Jessica")
    //        .WithTcpServer("mqtt.ably.io", 8883)
    //        .WithCredentials("GIn8xA.lwvRbg", "mmkCycAbnYQvmZxS")
    //        .WithTls()
    //        .WithCleanSession()
    //        .Build();

    //    System.Threading.CancellationToken cancellationToken;
    //    await mqttClient.ConnectAsync(options, cancellationToken);

    //    var message = new MqttApplicationMessageBuilder()
    //    .WithTopic("suitceyes/tactile-board/test")
    //    .WithPayLoad("1")
    //    //.WithExactlyOceQoS()
    //    //.WithRetainFlag()
    //    .Build();

    //    await mqttClient.PublishAsync(message, cancellationToken);
    //}

    // Start is called before the first frame update
    void Start()
    {
        //SendMQTT();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
