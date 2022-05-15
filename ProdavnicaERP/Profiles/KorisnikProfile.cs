using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;

namespace ProdavnicaERP.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile() {

            CreateMap<Korisnik, KorisnikDto>();
            CreateMap<KorisnikDto, Korisnik>();
            CreateMap<KorisnikUpdateDto, Korisnik>();
            CreateMap<Korisnik, Korisnik>();
            CreateMap<KorisnikCreationDto, Korisnik>();
            CreateMap<Korisnik, KorisnikCreationDto>();
            CreateMap<Korisnik, KorisnikKorpaDto>();
            CreateMap<KorisnikDto, KorisnikKorpaDto>();
            CreateMap<KorisnikKorpaDto, Korisnik>();
            CreateMap<KorisnikKorpaDto, KorisnikDto>();
            }
    }
}
