using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IProizvodRepository
    {
        List<Proizvod> GetProizvod();

        Proizvod GetProizvodById(int ProizvodID);

        Proizvod CreateProizvod(Proizvod Proizvod);

        void UpdateProizvod(Proizvod Proizvod);

        void DeleteProizvod(int ProizvodID);
        public bool SaveChanges();
    }
}
