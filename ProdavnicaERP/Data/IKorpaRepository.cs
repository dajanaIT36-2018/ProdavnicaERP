using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IKorpaRepository
    {
        List<Korpa> GetKorpa();

        Korpa GetKorpaById(int KorpaID);
        Korpa GetKorpaByKorisnikId(int korisnikId);

        Korpa CreateKorpa(Korpa Korpa);

        void UpdateKorpa(Korpa Korpa);

        void DeleteKorpa(int KorpaID);
        public bool SaveChanges();
    }
}
