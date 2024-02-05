using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppService.Services
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetMapperConfiguration<TFrom, TTo>()
                                                where TFrom : class
                                                where TTo : class
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFrom, TTo>();
            });

            return configuration.CreateMapper();
        }
    }
}
