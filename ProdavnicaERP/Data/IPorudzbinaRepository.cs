using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Data
{
    public interface IPorudzbinaRepository
    {
        List<Porudzbina> GetPorudzbina();

        Porudzbina GetPorudzbinaById(int PorudzbinaID);

        Porudzbina CreatePorudzbina(Porudzbina Porudzbina);

        Porudzbina GetPorudzbineByKorisnik(int korisnikId);

        Porudzbina GetPorudzbineByStatus(int korisnikId);
        void UpdatePorudzbina(Porudzbina Porudzbina);

        void DeletePorudzbina(int PorudzbinaID);
        public bool SaveChanges();
    }
}
