using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class ProizvodCreationDto
    {
        public int TipProizvodaId { get; set; }
        public int VrstaProizvodaId { get; set; }
        public int ProizvodjacId { get; set; }
        public int? Velicina { get; set; }
        public decimal Cena { get; set; }
        public int? Kolicina { get; set; }
        public int? Popust { get; set; }
        public string Slika { get; set; }

    }
}
