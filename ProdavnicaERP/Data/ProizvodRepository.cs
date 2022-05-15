using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly WEBSHOPContext context;
        public ProizvodRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Proizvod> GetProizvod()
        {
            return (from sl in context.Proizvods select sl).ToList();
        }

        public Proizvod GetProizvodById(int ProizvodID)
        {
            return context.Proizvods.FirstOrDefault(k => k.ProizvodId == ProizvodID);
        }

        public Proizvod CreateProizvod(Proizvod Proizvod)
        {
            context.Proizvods.Add(Proizvod);
            return Proizvod;
        }

        public void UpdateProizvod(Proizvod Proizvod)
        {
            throw new NotImplementedException();
        }

        public void DeleteProizvod(int ProizvodID)
        {
            context.Proizvods.Remove(context.Proizvods.FirstOrDefault(sl => sl.ProizvodId == ProizvodID));
        }
    }
}
