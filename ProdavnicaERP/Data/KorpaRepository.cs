using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class KorpaRepository : IKorpaRepository
    {
        private readonly WEBSHOPContext context;
        public KorpaRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Korpa> GetKorpa()
        {
            return (from sl in context.Korpas select sl).ToList();
        }

        public Korpa GetKorpaById(int KorpaID)
        {
            return context.Korpas.FirstOrDefault(k => k.KorpaId == KorpaID);
        }
        public Korpa GetKorpaByKorisnikId(int korisnikId)
        {
            return context.Korpas.FirstOrDefault(k => k.KorisnikId == korisnikId);
        }
        public Korpa CreateKorpa(Korpa Korpa)
        {
            context.Korpas.Add(Korpa);
            return Korpa;
        }

        public void UpdateKorpa(Korpa Korpa)
        {
            throw new NotImplementedException();
        }

        public void DeleteKorpa(int KorpaID)
        {
            context.Korpas.Remove(context.Korpas.FirstOrDefault(sl => sl.KorpaId == KorpaID));
        }

    }
}
