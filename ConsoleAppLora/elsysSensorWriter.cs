using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elsys.Decoder;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace ConsoleAppLora
{
    public static class elsysSensorWriter
    {
        private const string DEVICE_ID = "deviceid";
        private const string TEMPERATURE = "temperature";
        private const string HUMIDITY = "humidity";
        private const string LIGHT = "light";
        private const string MOTION = "motion";
        private const string CO2 = "co2";
        private const string VDD = "vdd";
        private const string XX = "x";
        private const string YY = "y";
        private const string ZZ = "z";
        private const string ACC_MOTION = "accmotion";
        private const string PRESSURE = "pressure";
        private const string EXTERNAL_TEMPERATURE = "externalTemperature";
        private const string TIME_STAMP = "timeStamp";



        public static async Task<InfluxDBClient> Main(string[] args)
        {
            var client = new InfluxDBClient("http://localhost:9999","my-user", "my-password");
            return client;
        }

        private static void WriteElt2Point(InfluxDBClient influxDB,WriteOptions writeOptions, Data data, string deviceId)
        {



            //var client = new InfluxDBClient("http://localhost:9999",
            //           "my-user", "my-password");


            using (var writeClient = influxDB.GetWriteApi(writeOptions))
            {
                if (writeClient != null)
                {
                    DateTime dt = DateTime.UtcNow.AddSeconds(-10);

                    var point = PointData.Measurement("ELT2")
                        .Tag("DEVICE_ID", deviceId)
                        .Field("TEMPERATURE", data.Temperature)
                        .Timestamp(dt, WritePrecision.Ms);
                    writeClient.WritePoint(point, "elt2", deviceId);

                }

            }
        }
    }
}
