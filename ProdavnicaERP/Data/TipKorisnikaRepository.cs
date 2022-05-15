using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class TipKorisnikaRepository : ITipKorisnikaRepository
    {
        private readonly WEBSHOPContext context;
        public TipKorisnikaRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<TipKorisnika> GetTipKorisnika()
        {
            return (from sl in context.TipKorisnikas select sl).ToList();
        }

        public TipKorisnika GetTipKorisnikaById(int TipKorisnikaID)
        {
            return context.TipKorisnikas.FirstOrDefault(k => k.TipKorisnikaId == TipKorisnikaID);
        }

        public TipKorisnika CreateTipKorisnika(TipKorisnika TipKorisnika)
        {
            context.TipKorisnikas.Add(TipKorisnika);
            return TipKorisnika;
        }

        public void UpdateTipKorisnika(TipKorisnika TipKorisnika)
        {
            throw new NotImplementedException();
        }

        public void DeleteTipKorisnika(int TipKorisnikaID)
        {
            context.TipKorisnikas.Remove(context.TipKorisnikas.FirstOrDefault(sl => sl.TipKorisnikaId == TipKorisnikaID));
        }
    }
}
