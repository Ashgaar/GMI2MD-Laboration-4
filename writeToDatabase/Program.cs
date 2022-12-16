using System;
using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace Examples
{
    public class Examples
    {
        public static async Task Main(string[] args)
        {
            // You can generate an API token from the "API Tokens Tab" in the UI
            var token = "dUkMcGQtTzGKdBEvXWeZpJkI7E2ZckpBqsSk1lvpgKBtMDbCVEzxlT8OHviYkBvv2bTiex54J8kXxq4r-__FzA==";
            const string bucket = "ec2";
            const string org = "darkness";

            using var client = InfluxDBClientFactory.Create("http://localhost:8086", token);


            var point = PointData
                  .Measurement("mem")
                  .Tag("host", "host1")
                  .Field("used_percent", 27.4390)
                  .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            using (var writeApi = client.GetWriteApi())
            {
                writeApi.WritePoint(point, bucket, org);
            }

        }
    }
}
