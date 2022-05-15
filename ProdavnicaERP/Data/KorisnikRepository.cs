using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly WEBSHOPContext context;
        public KorisnikRepository(WEBSHOPContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<Korisnik> GetKorisnik()
        {
            return (from sl in context.Korisniks select sl).ToList();
        }

        public Korisnik GetKorisnikById(int korisnikID)
        {
            return context.Korisniks.FirstOrDefault(k => k.KorisnikId == korisnikID);
        }

        public Korisnik CreateKorisnik(Korisnik korisnik)
        {
            context.Korisniks.Add(korisnik);
            return korisnik;
        }

        public void UpdateKorisnik(Korisnik korisnik)
        {
            throw new NotImplementedException();
        }

        public void DeleteKorisnik(int korisnikID)
        {
            context.Korisniks.Remove(context.Korisniks.FirstOrDefault(sl => sl.KorisnikId == korisnikID));
        }
        public Korisnik GetKorisnikByUserNameAndPassword(string korisnickoIme, string lozinka) 
        {
            return context.Korisniks.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme & k.Lozinka == lozinka) ;
        }
    }

}
