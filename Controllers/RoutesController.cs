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
using ProjectWebAPI.Repository;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        public DynamoDBContext context { get; }
        public BasicAWSCredentials credentials { get; }

        public static AmazonDynamoDBClient client = null;
       
        
        private readonly IBusRepository _busRepository;
        public RoutesController(IBusRepository busRepository)
        {


            //var builder = new ConfigurationBuilder()
            //                .SetBasePath(Directory.GetCurrentDirectory())
            //                .AddJsonFile("AppSettings.Json", optional: true, reloadOnChange: true);

            //var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            //var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            //var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            //client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            //context = new DynamoDBContext(client);

            //var config = new MapperConfiguration(cfg =>
            //       {
            //           cfg.CreateMap<Bus, BusDTO>();
            //           cfg.CreateMap<BusDTO, Bus>();
            //           cfg.CreateMap<BusStop, BusStopDTO>();
            //           cfg.CreateMap<BusStopDTO, BusStop>();
            //       }
            //   );
            //mapper = config.CreateMapper();

            _busRepository = busRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            //var conditions = new List<ScanCondition>();

            //var bus = await context.ScanAsync<Bus>(conditions).GetRemainingAsync();
            //var busDTO = mapper.Map<List<BusDTO>>(bus);
           var busDTO= await _busRepository.GetRoutes();
            return Ok(busDTO);

        }

        [HttpGet]
        [Route("GetBusStopByRouteID/{RouteId}")]
        public async Task<IActionResult> GetBusStopByRouteID(string RouteId)
        {

            var busDTO = await _busRepository.GetBusStopByRouteID(RouteId);

            return Ok(busDTO);

        }

        [HttpGet]
        [Route("GetScheduleTimeByRouteId/{RouteId}")]
        public async Task<IActionResult> GetScheduleTimeByRouteId(string RouteId)
        {
            var obj = await _busRepository.GetScheduleTimeByRouteId(RouteId);

            return Ok(obj);

        }

        [HttpPost]
        [Route("AddRoute")]
        public async Task<IActionResult> AddRoute([FromBody] BusDTO busDTO )
        {

            var obj = await _busRepository.AddRoute(busDTO);
            return Ok(obj);

        }

        [HttpGet]
        [Route("GetRouteById/{RouteId}")]
        public async Task<IActionResult> GetRouteById(string RouteId)
        {
            var obj = await _busRepository.GetRouteById(RouteId);

            return Ok(obj);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoute([FromForm] BusDTO busDTO)
        [HttpPost]
        [Route("AddBusStop")]
        public async Task<IActionResult> AddBusStop([FromBody] BusStopDTO busStopDTO)
        {

            var obj = await _busRepository.AddBusStop(busStopDTO);
            return Ok(obj);

        }

        [HttpPut]
        [Route("UpdateRoute")]
        public async Task<IActionResult> UpdateRoute([FromBody] BusDTO busDTO)
        {

            var obj = await _busRepository.UpdateRoute(busDTO);
            return Ok(obj);

        }

        [HttpDelete("{id}")]      
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var obj = await _busRepository.GetRouteById(id);

            await _busRepository.DeleteRoute(obj);
            return Ok("Successfully deleted");
        }

        [HttpPut]
        [Route("UpdateBusStop")]
        public async Task<IActionResult> UpdateBusStop([FromBody] BusDTO busDTO)
        {

            var obj = await _busRepository.UpdateRoute(busDTO);
            return Ok(obj);

        }

        [HttpDelete]
        [Route("DeleteBusStop")]
        public async Task<IActionResult> DeleteBusStop([FromBody] BusDTO busDTO)
        {
            await _busRepository.DeleteRoute(busDTO);
            return Ok("Successfully deleted");

        }
    }
}
