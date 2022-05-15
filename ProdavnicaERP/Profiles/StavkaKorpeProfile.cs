using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class StavkaKorpeProfile : Profile
    {
        public StavkaKorpeProfile()
        {

            CreateMap<StavkaKorpe, StavkaKorpeDto>();
            CreateMap<StavkaKorpeDto, StavkaKorpe>();
            CreateMap<StavkaKorpeUpdateDto, StavkaKorpe>();
            CreateMap<StavkaKorpe, StavkaKorpe>();
            CreateMap<StavkaKorpeCreationDto, StavkaKorpe>();
            CreateMap<StavkaKorpe, StavkaKorpeCreationDto>();
        }

    }
}
