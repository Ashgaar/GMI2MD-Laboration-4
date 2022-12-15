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
using Newtonsoft.Json.Linq;

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




        //public static async Task Main(string[] args)
        //{
        //    // You can generate an API token from the "API Tokens Tab" in the UI
        //    //var token = Environment.GetEnvironmentVariable("INFLUX_TOKEN")!;


        //    var client = InfluxDBClientFactory.Create("http://localhost:8086", token);
        //}


        public static void Write(Action<WriteApi> action)
        {

            var token = "dUkMcGQtTzGKdBEvXWeZpJkI7E2ZckpBqsSk1lvpgKBtMDbCVEzxlT8OHviYkBvv2bTiex54J8kXxq4r-__FzA==";

            var writeOptions = new WriteOptions
            {
                BatchSize = 5000,
                FlushInterval = 1000,
                JitterInterval = 1000,
                RetryInterval = 5000
            };
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", token);
            using var write = client.GetWriteApi(writeOptions);
            action(write);
        }

        public static void WriteElt2Point(/*InfluxDBClient influxDB,*//*WriteOptions writeOptions,*/ Data data, string deviceId)
        {


            Write(write =>
            {
                DateTime dt = DateTime.UtcNow.AddSeconds(-10);

                var point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(ACC_MOTION, data.AccMotion)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");


                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(EXTERNAL_TEMPERATURE, data.ExternalTemperature)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");



                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(HUMIDITY, data.Humidity)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");



                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(PRESSURE, data.Pressure)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");

                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(TEMPERATURE, data.Temperature)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");

                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(VDD, data.Vdd)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");

                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(XX, data.X)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");

                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(YY, data.Y)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");

                point = PointData.Measurement("elt2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(ZZ, data.Z)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "elt2", "darkness");


            });


            //using (var writeClient = influxDB.GetWriteApi(writeOptions))
            //{
            //    if (writeClient != null)
            //    {
            //        DateTime dt = DateTime.UtcNow.AddSeconds(-10);

            //        var point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(ACC_MOTION, data.AccMotion)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");


            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(EXTERNAL_TEMPERATURE, data.ExternalTemperature)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");



            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(HUMIDITY, data.Humidity)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");



            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(PRESSURE, data.Pressure)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(TEMPERATURE, data.Temperature)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(VDD, data.Vdd)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(XX, data.X)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(YY, data.Y)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //        point = PointData.Measurement("elt2")
            //            .Tag("DEVICE_ID", deviceId)
            //            .Field(ZZ, data.Z)
            //            .Timestamp(dt, WritePrecision.Ms);
            //        writeClient.WritePoint(point, "elt2", "darkness");

            //    }

            //}
        }

        public static void WriteEc2Point(/*InfluxDBClient influxDB,*/ /*WriteOptions writeOptions,*/ Data data, string deviceId)
        {
            Write(write =>
            {
                DateTime dt = DateTime.UtcNow.AddSeconds(-10);

                var point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(CO2, data.Co2)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

                point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(HUMIDITY, data.Humidity)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

                point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(LIGHT, data.Light)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

                point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(MOTION, data.Motion)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

                point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(TEMPERATURE, data.Temperature)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

                point = PointData.Measurement("ec2")
                    .Tag("DEVICE_ID", deviceId)
                    .Field(VDD, data.Vdd)
                    .Timestamp(dt, WritePrecision.Ms);
                write.WritePoint(point, "ec2", "darkness");

            });

            //    var writeOptions = new WriteOptions
            //    {
            //        BatchSize = 5000,
            //        FlushInterval = 1000,
            //        JitterInterval = 1000,
            //        RetryInterval = 5000
            //    };

            //    //var influxDB = new InfluxDBClient("http://localhost:8086",
            //    //           "ashgaar", "ashgaar123");


            //    using (var writeClient = influxDB.GetWriteApi(writeOptions))
            //    {
            //        if (writeClient != null)
            //        {
            //            DateTime dt = DateTime.UtcNow.AddSeconds(-10);

            //            var point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(CO2, data.Co2)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");

            //            point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(HUMIDITY, data.Humidity)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");

            //            point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(LIGHT, data.Light)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");

            //            point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(MOTION, data.Motion)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");

            //            point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(TEMPERATURE, data.Temperature)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");

            //            point = PointData.Measurement("ec2")
            //                .Tag("DEVICE_ID", deviceId)
            //                .Field(VDD, data.Vdd)
            //                .Timestamp(dt, WritePrecision.Ms);
            //            writeClient.WritePoint(point, "ec2", "darkness");


            //        }

            //    }
            //}
        }
    }
}
