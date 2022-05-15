using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class TipProizvodumProfile : Profile
    {
        public TipProizvodumProfile()
        {

            CreateMap<TipProizvodum, TipProizvodumDto>();
            CreateMap<TipProizvodumDto, TipProizvodum>();
            CreateMap<TipProizvodumUpdateDto, TipProizvodum>();
            CreateMap<TipProizvodum, TipProizvodum>();
            CreateMap<TipProizvodumCreationDto, TipProizvodum>();
            CreateMap<TipProizvodum, TipProizvodumCreationDto>();
        }

    }
}
