using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class StavkaPorudzbineDto
    {
    
        public int KolicinaStavkePorudzbine { get; set; }
        public int VelicinaStavkePorudzbine { get; set; }
        public decimal UkupnaCenaStavkePorudzbine { get; set; }

        public virtual PorudzbinaDto Porudzbina { get; set; }
        public virtual ProizvodDto Proizvod { get; set; }
    }
}
