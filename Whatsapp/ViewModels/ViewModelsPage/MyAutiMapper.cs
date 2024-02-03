using AutoMapper;
using ChatAppModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp.Dtos;

namespace Whatsapp.ViewModels.ViewModelsPage
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User,UserDto>();
            });

            return configuration.CreateMapper();
        }
    }
}
