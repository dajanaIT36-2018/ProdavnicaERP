using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class StavkaPorudzbineProfile : Profile
    {
        public StavkaPorudzbineProfile()
        {

            CreateMap<StavkaPorudzbine, StavkaPorudzbineDto>();
            CreateMap<StavkaPorudzbineDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbineUpdateDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbineCreationDto, StavkaPorudzbine>();
            CreateMap<StavkaPorudzbine, StavkaPorudzbineCreationDto>();
        }

    }
}
