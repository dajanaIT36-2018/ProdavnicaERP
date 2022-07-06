using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class StavkaKorpeDto
    {

        public int StavkaKorpeId { get; set; }
        public int KolicinaStavke { get; set; }
        public int VelicinaStavke { get; set; }
        public decimal UkupnaCenaStavke { get; set; }
        public int KorpaId { get; set; }
        public int ProizvodId { get; set; }
        public virtual KorpaDto Korpa { get; set; }
        public virtual ProizvodDto Proizvod { get; set; }
    }
}
