using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class VrstaProizvodumProfile : Profile
    {
        public VrstaProizvodumProfile()
        {

            CreateMap<VrstaProizvodum, VrstaProizvodumDto>();
            CreateMap<VrstaProizvodumDto, VrstaProizvodum>();
            CreateMap<VrstaProizvodumUpdateDto, VrstaProizvodum>();
            CreateMap<VrstaProizvodum, VrstaProizvodum>();
            CreateMap<VrstaProizvodumCreationDto, VrstaProizvodum>();
            CreateMap<VrstaProizvodum, VrstaProizvodumCreationDto>();
        }


    }
}
