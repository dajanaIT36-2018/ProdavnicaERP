using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class StavkaPorudzbineCreationDto
    {
       
        public int ProizvodId { get; set; }
        public int PorudzbinaId { get; set; }
        public int KolicinaStavkePorudzbine { get; set; }
        public int VelicinaStavkePorudzbine { get; set; }
        public decimal UkupnaCenaStavkePorudzbine { get; set; }

       
    }
}
