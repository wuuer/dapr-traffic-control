using Simulation.Proxies;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mqttServer = System.Net.Mqtt.MqttServer.Create(1883);


            //mqttServer.ClientConnected += MqttServer_ClientConnected;
            //mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
            //mqttServer.Start();


            int lanes = 3;
            CameraSimulation[] cameras = new CameraSimulation[lanes];
            for (var i = 0; i < lanes; i++)
            {
                int camNumber = i + 1;
                var trafficControlService = new MqttTrafficControlService(camNumber);

                cameras[i] = new CameraSimulation(camNumber, trafficControlService);
            }
            Parallel.ForEach(cameras, cam => cam.Start());

            Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();
        }

        private static void MqttServer_ClientDisconnected(object sender, string e)
        {
            Console.WriteLine($"{e} client Disconnected");
        }

        private static void MqttServer_ClientConnected(object sender, string e)
        {
            Console.WriteLine($"{e} client Connected");
        }
    }
}
