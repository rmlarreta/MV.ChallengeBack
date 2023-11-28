using AutoMapper;
using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Domain.AggregateRoot.Values;
using WeatherRequest.Infraestructure.Entities.Bd;
using static WeatherRequest.Infraestructure.Entities.Api.WeatherReport;

namespace WeatherRequest.Infraestructure.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ReportApi, WeatherReportBd>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.main.temp))
                .ForMember(dest => dest.Termica, opt => opt.MapFrom(src => src.main.feels_like))
                .ForMember(dest => dest.Validez, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.dt).DateTime));

            CreateMap<WeatherRequestCity, WeatherReportBd>()
                .ForMember(dest => dest.Temperatura, opt => opt.MapFrom(src => src.Clima))
                .ForPath(dest => dest.CityBd, opt => opt.MapFrom(src => src.Ciudad))
                .ReverseMap();         

            CreateMap<Ciudad, CityBd>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
              .ForMember(dest => dest.IdDouble, opt => opt.MapFrom(src => src.IdDouble))
              .ReverseMap();
        } 
    }
}
