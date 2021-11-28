using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using ProjectWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Repository
{
    public class BusRepository : IBusRepository
    {
        public IDynamoDBContext _context { get; }
        private readonly IMapper _mapper;
        Random _random = new Random();
        public BusRepository(IDynamoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Bus> AddRoute(BusDTO busDTO)
        {
            var busobj = _mapper.Map<Bus>(busDTO);
            busobj.BusID = _random.Next(10000000).ToString();

            await _context.SaveAsync(busobj);
            return busobj;
        }

        public async Task DeleteRoute(BusDTO busDTO)
        {
            var busobj = _mapper.Map<Bus>(busDTO);
            await _context.DeleteAsync(busobj);
        }
        public async Task DeleteBusStop(BusStopDTO busStopDTO)
        {
            var busobj = _mapper.Map<BusStop>(busStopDTO);
            await _context.DeleteAsync(busobj);
        }
        public async Task<BusStop> AddBusStop(BusStopDTO busDTO)
        {
            var busobj = _mapper.Map<BusStop>(busDTO);
            busobj.BusStopID = _random.Next(10000000).ToString();
            
            await _context.SaveAsync(busobj);
            return busobj;
        }

        public async Task<BusStopDTO> GetBusStopById(string BusStopId)
        {
            var busStop = await _context.ScanAsync<BusStop>(new[] {
                    new ScanCondition("BusStopID", ScanOperator.Equal, BusStopId)
                }).GetRemainingAsync();

            var busStopDTO = _mapper.Map<BusStopDTO>(busStop.FirstOrDefault());

            return busStopDTO;
        }

           public async Task<List<BusStopDTO>> GetBusStopByRouteID(string RouteId)
        {
            var bus = await _context.ScanAsync<BusStop>(new[] {
                    new ScanCondition("BusID", ScanOperator.Equal, RouteId)
                }).GetRemainingAsync();
            var busDTO = _mapper.Map<List<BusStopDTO>>(bus);
            return busDTO?.OrderBy(c => c.Order)?.ToList();
        }
		//alternative method of getRouteById
		public async Task<List<BusDTO>> GetRouteByPlace(string FromPlace, string ToPlace)
        {
            var bus = await _context.ScanAsync<Bus>(new[] {
                    new ScanCondition("StartPlace", ScanOperator.Contains, FromPlace),
                    new ScanCondition("EndPlace", ScanOperator.Contains, ToPlace)
                }).GetRemainingAsync();
            var busDTO = _mapper.Map<List<BusDTO>>(bus);
            return busDTO;
        }
         
		 //alternative method of getRouteById user can get using bus number
        public async Task<List<BusDTO>> GetRouteByBusNumber(string BusNumber)
        {
            var bus = await _context.ScanAsync<Bus>(new[] {
                    new ScanCondition("BusNumber", ScanOperator.Contains, BusNumber)
                }).GetRemainingAsync();
            var busDTO = _mapper.Map<List<BusDTO>>(bus);
            return busDTO;
        }

        public async Task<List<BusDTO>> GetRoutes()
        {
            var conditions = new List<ScanCondition>();
            var bus = await _context.ScanAsync<Bus>(conditions).GetRemainingAsync();
            var busDTO = _mapper.Map<List<BusDTO>>(bus);
            return busDTO;
        }
         
        public async Task<BusDTO> GetRouteById(string RouteId)
        {
            var bus = await _context.ScanAsync<Bus>(new[] {
                    new ScanCondition("BusID", ScanOperator.Equal, RouteId)
                }).GetRemainingAsync();

            var busDTO = _mapper.Map<BusDTO>(bus.FirstOrDefault());

            return busDTO;
        }

        public async Task<BusscheduleViewModel> GetScheduleTimeByRouteId(string RouteId)
        {
            var bus = await _context.ScanAsync<Bus>(new[] {
                    new ScanCondition("BusID", ScanOperator.Equal, RouteId)
                }).GetRemainingAsync();
            var busDTO = _mapper.Map<BusDTO>(bus.FirstOrDefault());

            DateTime ts = DateTime.UtcNow;

            var startTime = new DateTime(ts.Year, ts.Month, ts.Day, Convert.ToInt16(busDTO.StartTime.Split(':')[0]), Convert.ToInt16(busDTO.StartTime.Split(':')[1]), 0);
            var stopTime = new DateTime(ts.Year, ts.Month, ts.Day, Convert.ToInt16(busDTO.EndTime.Split(':')[0]), Convert.ToInt16(busDTO.EndTime.Split(':')[1]), 0);

            BusscheduleViewModel obj = new BusscheduleViewModel();
            obj.BusID = busDTO.BusID;
            obj.BusName = busDTO.BusName;
            obj.BusNumber = busDTO.BusName;
            obj.StartPlace = busDTO.StartPlace;
            obj.EndPlace = busDTO.EndPlace;
            List<string> list = new List<string>();

            do
            {
                list.Add(startTime.ToString("HH:mm"));
                TimeSpan time = new TimeSpan(0, Convert.ToInt16(busDTO.Frequency.Split(':')[0]), Convert.ToInt16(busDTO.Frequency.Split(':')[1]), 0);
                startTime = startTime.Add(time);


            } while (startTime < stopTime);

            obj.ScheduledTime = list;

            return obj;
        }

        public async Task<Bus> UpdateRoute(BusDTO busDTO)
        {
            var busobj = _mapper.Map<Bus>(busDTO);

            await _context.SaveAsync(busobj);
            return busobj;
        }

        public async Task<BusStop> UpdateBusStop(BusStopDTO busStopDTO)
        {
            var busobj = _mapper.Map<BusStop>(busStopDTO);

            await _context.SaveAsync(busobj);
            return busobj;
        }
    }
}
