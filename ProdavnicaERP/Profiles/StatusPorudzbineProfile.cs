using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class StatusPorudzbineProfile : Profile
    {
        public StatusPorudzbineProfile()
        {

            CreateMap<StatusPorudzbine, StatusPorudzbineDto>();
            CreateMap<StatusPorudzbineDto, StatusPorudzbine>();
            CreateMap<StatusPorudzbineUpdateDto, StatusPorudzbine>();
            CreateMap<StatusPorudzbine, StatusPorudzbine>();
            CreateMap<StatusPorudzbineCreationDto, StatusPorudzbine>();
            CreateMap<StatusPorudzbine, StatusPorudzbineCreationDto>();
        }

    }
}
