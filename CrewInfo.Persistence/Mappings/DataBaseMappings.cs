using AutoMapper;
using CrewInfo.Persistence.Entities;
using CrewInfo.Core.Models;

namespace CrewInfo.Persistence.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<PilotEntity, Pilot>();
            CreateMap<Pilot, PilotEntity>();
            CreateMap<StewardEntity, Steward>();
            CreateMap<Steward, StewardEntity>();
        }
    }
}
