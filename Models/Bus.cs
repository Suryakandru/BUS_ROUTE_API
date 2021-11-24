using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
namespace ProjectWebAPI.Models
{
    [DynamoDBTable("Bus")]
    public class Bus
    {
        [DynamoDBProperty("BusID")]
        [DynamoDBHashKey]
        public string BusID { get; set; }

        [DynamoDBProperty("BusName")]
        public string BusName { get; set; }

        [DynamoDBProperty("Frequency")]
        public string Frequency { get; set; }

        [DynamoDBProperty("BusNumber")]
        public string BusNumber { get; set; }

        [DynamoDBProperty("StartTime")]
        public string StartTime { get; set; }

        [DynamoDBProperty("EndTime")]
        public string EndTime { get; set; }

        [DynamoDBProperty("StartPlace")]
        public string StartPlace { get; set; }

        [DynamoDBProperty("EndPlace")]
        public string EndPlace { get; set; }        

    }
}
