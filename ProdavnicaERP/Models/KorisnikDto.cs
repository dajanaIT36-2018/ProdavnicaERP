using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class KorisnikDto
    {
        public string ImeKorisnik { get; set; }
        public string PrezimeKorisnika { get; set; }
        public int? TelefonKorisnika { get; set; }
        public string EMailKorisnika { get; set; }
        public string GradKorisnika { get; set; }
        public string UlicaKorisnika { get; set; }
        public string BrUliceKorisnika { get; set; }
        public decimal? PostanskiBroj { get; set; }
        public string Pol { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnikaDto TipKorisnika { get; set; }
    }
}
