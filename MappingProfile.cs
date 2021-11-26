using AutoMapper;
using ProjectWebAPI.Models;

namespace ProjectWebAPI
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Bus, BusDTO>();
            CreateMap<BusDTO, Bus>();
            CreateMap<BusStop, BusStopDTO>();
            CreateMap<BusStopDTO, BusStop>();
        }
    }
}
