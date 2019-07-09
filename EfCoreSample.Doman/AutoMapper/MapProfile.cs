using AutoMapper;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;


namespace EfCoreSample.Doman.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProjectDTO, Project>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetDescriptionFromEnumValue(s.Status)));
            CreateMap<Project, ProjectDTO>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetEnumValueFromDescription<EProjectStatus>(s.Status)));

            CreateMap<SaveProjectDTO, Project>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetDescriptionFromEnumValue(s.Status)));
            CreateMap<Project, SaveProjectDTO>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetEnumValueFromDescription<EProjectStatus>(s.Status)));

        }

    }
}
