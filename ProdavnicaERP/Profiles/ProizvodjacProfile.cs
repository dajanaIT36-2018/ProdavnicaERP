using AutoMapper;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Profiles
{
    public class ProizvodjacProfile: Profile
    {
      public ProizvodjacProfile()
         {

        CreateMap<Proizvodjac, ProizvodjacDto>();
        CreateMap<ProizvodjacDto, Proizvodjac>();
        CreateMap<ProizvodjacUpdateDto, Proizvodjac>();
        CreateMap<Proizvodjac, Proizvodjac>();
        CreateMap<ProizvodjacCreationDto, Proizvodjac>();
        CreateMap<Proizvodjac, ProizvodjacCreationDto>();
    }

}
}
