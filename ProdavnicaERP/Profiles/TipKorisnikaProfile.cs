using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class TipKorisnikaProfile : Profile
    {
        public TipKorisnikaProfile()
        {

            CreateMap<TipKorisnika, TipKorisnikaDto>();
            CreateMap<TipKorisnikaDto, TipKorisnika>();
            CreateMap<TipKorisnikaUpdateDto, TipKorisnika>();
            CreateMap<TipKorisnika, TipKorisnika>();
            CreateMap<TipKorisnikaCreationDto, TipKorisnika>();
            CreateMap<TipKorisnika, TipKorisnikaCreationDto>();
        }

    }
}
