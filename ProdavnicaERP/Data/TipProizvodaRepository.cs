using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class TipProizvodaRepository : ITipProizvodaRepository
    {
        private readonly WEBSHOPContext context;
        public TipProizvodaRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<TipProizvodum> GetTipProizvoda()
        {
            return (from sl in context.TipProizvoda select sl).ToList();
        }

        public TipProizvodum GetTipProizvodaById(int TipProizvodaID)
        {
            return context.TipProizvoda.FirstOrDefault(k => k.TipProizvodaId == TipProizvodaID);
        }

        public TipProizvodum CreateTipProizvoda(TipProizvodum TipProizvoda)
        {
            context.TipProizvoda.Add(TipProizvoda);
            return TipProizvoda;
        }

        public void UpdateTipProizvoda(TipProizvodum TipProizvoda)
        {
            throw new NotImplementedException();
        }

        public void DeleteTipProizvoda(int TipProizvodaID)
        {
            context.TipProizvoda.Remove(context.TipProizvoda.FirstOrDefault(sl => sl.TipProizvodaId == TipProizvodaID));
        }
    }
}
