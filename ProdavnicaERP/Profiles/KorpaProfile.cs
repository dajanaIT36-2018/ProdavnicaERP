using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class KorpaProfile : Profile
    {
        public KorpaProfile()
        {

            CreateMap<Korpa, KorpaDto>();
            CreateMap<KorpaDto, Korpa>();
            CreateMap<KorpaUpdateDto, Korpa>();
            CreateMap<Korpa, Korpa>();
            CreateMap<KorpaCreationDto, Korpa>();
            CreateMap<Korpa, KorpaCreationDto>();

            CreateMap<Korpa, KorisnikKorpaDto>();
            CreateMap<KorisnikKorpaDto, Korpa>();
            CreateMap<KorisnikKorpaDto, KorpaDto>();
            CreateMap<KorpaDto, KorisnikKorpaDto>();

        }

    }
}
