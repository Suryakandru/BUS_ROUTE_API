using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectWebAPI.Models;
using AutoMapper;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Linq;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        public DynamoDBContext context { get; }
        public BasicAWSCredentials credentials { get; }

        public static AmazonDynamoDBClient client = null;
        IMapper mapper = null;
        public RoutesController()
        {


            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("AppSettings.Json", optional: true, reloadOnChange: true);

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            context = new DynamoDBContext(client);

            var config = new MapperConfiguration(cfg =>
                   {
                       cfg.CreateMap<Bus, BusViewModel>();
                       cfg.CreateMap<BusStop, BusStopViewModel>();
                   }
               );
            mapper = config.CreateMapper();
        }
        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            var conditions = new List<ScanCondition>();

            var bus = await context.ScanAsync<Bus>(conditions).GetRemainingAsync();
            var busDTO = mapper.Map<List<BusViewModel>>(bus);
            return Ok(busDTO);

        }

        [HttpGet]
        [Route("GetBusStopByRouteID/{RouteId}")]
        public async Task<IActionResult> GetBusStopByRouteID(string RouteId)
        {
            var conditions = new List<ScanCondition>();

            var bus = await context.ScanAsync<BusStop>(new[] {
                    new ScanCondition("BusID", ScanOperator.Equal, RouteId)
                }).GetRemainingAsync();
            var busDTO = mapper.Map<List<BusStopViewModel>>(bus);

            return Ok(busDTO);

        }

        [HttpGet]
        [Route("GetScheduleTimeByRouteId/{RouteId}")]
        public async Task<IActionResult> GetScheduleTimeByRouteId(string RouteId)
        {
            var conditions = new List<ScanCondition>();

            var bus = await context.ScanAsync<Bus>(new[] {
                    new ScanCondition("BusID", ScanOperator.Equal, RouteId)
                }).GetRemainingAsync();
            var busDTO = mapper.Map<BusViewModel>(bus.FirstOrDefault());

            DateTime ts = DateTime.UtcNow;

            var startTime = new DateTime(ts.Year, ts.Month, ts.Day, Convert.ToInt16(busDTO.StartTime.Split(':')[0]), Convert.ToInt16(busDTO.StartTime.Split(':')[1]), 0);
            var stopTime = new DateTime(ts.Year, ts.Month, ts.Day, Convert.ToInt16(busDTO.EndTime.Split(':')[0]), Convert.ToInt16(busDTO.EndTime.Split(':')[1]), 0);

            BusscheduleViewModel obj = new BusscheduleViewModel();
            obj.BusID = busDTO.BusID;
            obj.BusName = busDTO.BusName;
            obj.BusNumber = busDTO.BusName;
            obj.StartPlace = busDTO.StartPlace;
            obj.EndPlace = busDTO.EndPlace ;
            List<string> list = new List<string>();

            do
            {
                list.Add(startTime.ToString("HH:mm"));
                TimeSpan time = new TimeSpan(0, Convert.ToInt16(busDTO.Frequency.Split(':')[0]), Convert.ToInt16(busDTO.Frequency.Split(':')[1]), 0);
                startTime = startTime.Add(time);
               

            } while (startTime < stopTime);

            obj.ScheduledTime = list;

            return Ok(obj);

        }
    }
}
