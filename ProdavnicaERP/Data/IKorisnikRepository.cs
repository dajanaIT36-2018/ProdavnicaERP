using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetKorisnik();

        Korisnik GetKorisnikById(int korisnikID);

        Korisnik CreateKorisnik(Korisnik korisnik);

        void UpdateKorisnik(Korisnik korisnik);

        void DeleteKorisnik(int korisnikID);
        public bool SaveChanges();
    }
}
