using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IProizvodjacRepository
    {
        List<Proizvodjac> GetProizvodjac();

        Proizvodjac GetProizvodjacById(int ProizvodjacID);

        Proizvodjac CreateProizvodjac(Proizvodjac Proizvodjac);

        void UpdateProizvodjac(Proizvodjac Proizvodjac);

        void DeleteProizvodjac(int ProizvodjacID);
        public bool SaveChanges();
    }
}
