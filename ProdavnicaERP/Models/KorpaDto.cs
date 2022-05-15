using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class KorpaDto
    {
        public int KorpaId { get; set; }
        public KorisnikKorpaDto Korisnik { get; set; }
    }
}
