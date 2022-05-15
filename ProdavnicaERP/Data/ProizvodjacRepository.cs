using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class ProizvodjacRepository : IProizvodjacRepository
    {
        private readonly WEBSHOPContext context;
        public ProizvodjacRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Proizvodjac> GetProizvodjac()
        {
            return (from sl in context.Proizvodjacs select sl).ToList();
        }

        public Proizvodjac GetProizvodjacById(int ProizvodjacID)
        {
            return context.Proizvodjacs.FirstOrDefault(k => k.ProizvodjacId == ProizvodjacID);
        }

        public Proizvodjac CreateProizvodjac(Proizvodjac Proizvodjac)
        {
            context.Proizvodjacs.Add(Proizvodjac);
            return Proizvodjac;
        }

        public void UpdateProizvodjac(Proizvodjac Proizvodjac)
        {
            throw new NotImplementedException();
        }

        public void DeleteProizvodjac(int ProizvodjacID)
        {
            context.Proizvodjacs.Remove(context.Proizvodjacs.FirstOrDefault(sl => sl.ProizvodjacId == ProizvodjacID));
        }
    }
}
