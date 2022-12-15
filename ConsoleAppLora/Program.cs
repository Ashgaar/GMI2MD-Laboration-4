using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALoRa.Library;
using Parser;
using Elsys.Decoder;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using ConsoleAppLora;

namespace ConsoleApp.Lora
{
    public class Program
    {
        private static bool CONTAINER = false;
        static void Main(string[] args)
        {
            Console.WriteLine("\nALoRa ConsoleApp - A The Things Network C# Library\n");

            string TTN_APP_ID = "campusborlangeelsys";
            string TTN_ACCESS_KEY = "NNSXS.GTNTSDWU4ZBW365PKTHWGE4KL67KY75ZHVKCMZA.AC2JT7W3WIRZ3PVLQQXCAYFUTCWM5426VBVSAB7OC4GDBIC5SBQQ";
            string TTN_REGION = "eu1";

            

            using (var app = new TTNApplication(TTN_APP_ID, TTN_ACCESS_KEY, TTN_REGION))
            {
                app.MessageReceived += App_MessageReceived;

                if (CONTAINER)
                {
                    // use for testing when running as container
                    Thread.Sleep(Timeout.Infinite);
                }
                else
                {
                    Console.WriteLine("Press return to exit!");
                    Console.ReadLine();
                    Console.WriteLine("\nAloha, Goodbye, Vaarwel!");
                    Thread.Sleep(1000);
                }
                app.Dispose();
            }
        }

        private static void App_MessageReceived(TTNMessage obj)
        {
            var data = obj.Payload != null ? BitConverter.ToString(obj.Payload) : string.Empty;
            var deviceId = obj.DeviceID;
            Console.WriteLine($"Message Timestamp: {obj.Timestamp}, Device: {obj.DeviceID}, Topic: {obj.Topic}, Payload: {data}");


            var normalData = data.Replace("-", "");
            var cleanData = Elsys.Decoder.Decoder.Decode(normalData);

            DisplayMessage(deviceId,cleanData);
        }

        public static void DisplayMessage(string deviceId,Data cleanData){
            if(deviceId.EndsWith("a4") || deviceId.EndsWith("a5"))
            {
                Console.WriteLine($"Decoded messages: Co2 {cleanData.Co2}, Humidity {cleanData.Humidity}, Light {cleanData.Light}, Motion {cleanData.Motion}, Temperature {cleanData.Temperature}, Vdd {cleanData.Vdd}");

                elsysSensorWriter.WriteEc2Point(cleanData,deviceId);
            }
            
            if (deviceId.EndsWith("fd") || deviceId.EndsWith("fe"))
            {
                Console.WriteLine($"Decoded messages: AccMotion {cleanData.AccMotion}, ExternalTemperature {cleanData.ExternalTemperature}, Humidity {cleanData.Humidity}, Pressure {cleanData.Pressure}, Temperature {cleanData.Temperature}, Vdd {cleanData.Vdd}, X {cleanData.X}, Y {cleanData.Y}, Z {cleanData.Z}");


                elsysSensorWriter.WriteElt2Point(cleanData,deviceId);
            }
        }
    }
}
