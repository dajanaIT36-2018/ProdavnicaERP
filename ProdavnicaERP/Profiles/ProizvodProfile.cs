using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class ProizvodProfile : Profile
    {
        public ProizvodProfile()
        {

            CreateMap<Proizvod, ProizvodDto>();
            CreateMap<ProizvodDto, Proizvod>();
            CreateMap<ProizvodUpdateDto, Proizvod>();
            CreateMap<Proizvod, Proizvod>();
            CreateMap<ProizvodCreationDto, Proizvod>();
            CreateMap<Proizvod, ProizvodCreationDto>();
        }


    }
}
