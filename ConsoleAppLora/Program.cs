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
using System.Configuration;

namespace ConsoleApp.Lora
{
    public class Program
    {
        private static bool CONTAINER = false;
        static void Main(string[] args)
        {
            Console.WriteLine("\nALoRa ConsoleApp - A The Things Network C# Library\n");

            //string TTN_APP_ID = "campusborlangeelsys";
            //string TTN_ACCESS_KEY = "NNSXS.GTNTSDWU4ZBW365PKTHWGE4KL67KY75ZHVKCMZA.AC2JT7W3WIRZ3PVLQQXCAYFUTCWM5426VBVSAB7OC4GDBIC5SBQQ";
            //string TTN_REGION = "eu1";

            string TTN_APP_ID = GetAppSettingValue("TTN_APP_ID");
            string TTN_ACCESS_KEY = GetAppSettingValue("TTN_API_KEY");
            string TTN_REGION = GetAppSettingValue("TTN_REGION");



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
                    Thread.Sleep(Timeout.Infinite);
                    //Console.WriteLine("Press return to exit!");
                    //Console.ReadLine();
                    //Console.WriteLine("\nAloha, Goodbye, Vaarwel!");
                    //Thread.Sleep(1000);
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

        /// <summary>
        ///  /// Use this method for App.config files outside the app folder: https://stackoverflow.com/questions/10656077/what-is-wrong-with-my-app-config-file
        /// </summary>
        /// <param name="connectionStringKey"></param>
        /// <returns>connectionString value</returns>
        public static string GetConnectionStringValue(string connectionStringKey)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new();
                fileMap.ExeConfigFilename = "/vm/conf/App.config";

                var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var value = configuration.ConnectionStrings.ConnectionStrings[connectionStringKey].ConnectionString;

                //var value = ConfigurationManager.ConnectionStrings[connectionStringKey].ToString();
                if (string.IsNullOrEmpty(value))
                {
                    var message = $"Cannot find value for connectionString key: '{connectionStringKey}'.";
                    throw new ConfigurationErrorsException(message);
                }
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The connectionStringKey: {connectionStringKey} could not be read!");
                Console.WriteLine($"Exception: {e.Message}");
                return "";
            }
        }

        /// <summary>
        /// Use this method for App.config files outside the app folder: https://stackoverflow.com/questions/10656077/what-is-wrong-with-my-app-config-file
        /// </summary>
        /// <param name="appSettingKey"></param>
        /// <returns>Appsetting value</returns>
        public static string GetAppSettingValue(string appSettingKey)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new();
                fileMap.ExeConfigFilename = "/vm/conf/App.config";

                var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var value = configuration.AppSettings.Settings[appSettingKey].Value;

                //var value = ConfigurationManager.AppSettings[appSettingKey];
                if (string.IsNullOrEmpty(value))
                {
                    var message = $"Cannot find value for appSetting key: '{appSettingKey}'.";
                    throw new ConfigurationErrorsException(message);
                }
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The appSettingKey: {appSettingKey} could not be read!");
                Console.WriteLine($"Exception: {e.Message}");
                return "";
            }
        }

    }
}
