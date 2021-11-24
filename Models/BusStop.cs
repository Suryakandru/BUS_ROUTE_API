using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
namespace ProjectWebAPI.Models
{
    [DynamoDBTable("BusStop")]
    public class BusStop
    {
        [DynamoDBProperty("BusStopID")]
        [DynamoDBHashKey]
        public string BusStopID { get; set; }

        [DynamoDBProperty("BusStopName")]
        public string BusStopName { get; set; }

        [DynamoDBProperty("BusID")]
        public string BusID { get; set; }

        [DynamoDBProperty("Order")]
        public int Order { get; set; }
    }
}
