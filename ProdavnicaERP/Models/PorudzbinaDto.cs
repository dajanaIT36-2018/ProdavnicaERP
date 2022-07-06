using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class PorudzbinaDto
    {
        public int PorudzbinaId { get; set; }
        public string GradKorisnika { get; set; }
        public string UlicaKorisnika { get; set; }
        public string BrUliceKorisnika { get; set; }
        public decimal? PostanskiBroj { get; set; }
        public decimal IznosDostave { get; set; }
        public decimal UkupanIznosPorudzbine { get; set; }
        public string NacinPlacanja { get; set; }
        public DateTime? Datum { get; set; }

        public KorisnikDto Korisnik { get; set; }
        public StatusPorudzbineDto StatusPorudzbine { get; set; }
    }
}
