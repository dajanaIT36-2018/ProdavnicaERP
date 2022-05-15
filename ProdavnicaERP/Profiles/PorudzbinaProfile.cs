using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class PorudzbinaProfile : Profile
    {
        public PorudzbinaProfile()
        {

            CreateMap<Porudzbina, PorudzbinaDto>();
            CreateMap<PorudzbinaDto, Porudzbina>();
            CreateMap<PorudzbinaUpdateDto, Porudzbina>();
            CreateMap<Porudzbina, Porudzbina>();
            CreateMap<PorudzbinaCreationDto, Porudzbina>();
            CreateMap<Porudzbina, PorudzbinaCreationDto>();
        }

    }
}
