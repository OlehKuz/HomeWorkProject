using AutoMapper;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;


namespace EfCoreSample.Doman.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProjectPutDto, Project>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetDescriptionFromEnumValue(s.Status)));
            CreateMap<Project, ProjectPutDto>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetEnumValueFromDescription<EProjectStatus>(s.Status)));

            CreateMap<ProjectPostDto, Project>()
                    .ForMember(dest => dest.Status,
                        src => src.MapFrom(s => EnumExtention.GetDescriptionFromEnumValue(s.Status)));
            CreateMap<Project, ProjectGetDto>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
                
        }

    }
}
