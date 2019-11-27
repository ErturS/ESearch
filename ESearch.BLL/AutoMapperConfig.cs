using AutoMapper;
using ESearch.BLL.Entities;
using ESearch.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL
{
    public class AutoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfig>());
            Mapper.AssertConfigurationIsValid();
        }
    }
    class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<QueryResult, SearchResult>()
                .ForMember(x => x.Title, x => x.MapFrom(m => m.Title))
                .ForMember(x => x.Link, x => x.MapFrom(m => m.Link))
                .ForMember(x => x.Description, x => x.MapFrom(m => m.Description));
        }
    }
}
