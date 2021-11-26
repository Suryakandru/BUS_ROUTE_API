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
        Task DeleteRoute(BusDTO busDTO);
       
    }
}
