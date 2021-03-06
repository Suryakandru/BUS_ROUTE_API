using ProjectWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectWebAPI.Repository
{
    public interface IBusRepository
    {
        Task<List<BusDTO>> GetRoutes();
        Task<List<BusStopDTO>> GetBusStopByRouteID(string RouteId);
        Task<BusscheduleViewModel> GetScheduleTimeByRouteId(string RouteId);
        Task<Bus> AddRoute(BusDTO busDTO);
        Task<Bus> UpdateRoute(BusDTO busDTO);
        Task<BusDTO> GetRouteById(string RouteId);
        Task DeleteRoute(BusDTO busDTO);
        Task<BusStop> AddBusStop(BusStopDTO busDTO);
        Task<BusStopDTO> GetBusStopById(string BusStopId);
        Task<BusStop> UpdateBusStop(BusStopDTO busDTO);
        Task DeleteBusStop(BusStopDTO busStopDTO);
		//alternative to getRouteById
		Task<List<BusDTO>> GetRouteByPlace(string FromPlace, string ToPlace);
        Task<List<BusDTO>> GetRouteByBusNumber(string BusNumber);


    }
}
