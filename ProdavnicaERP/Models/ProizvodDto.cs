using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Models
{
    public class ProizvodDto
    {
        public int ProizvodId { get; set; }
        public int? Velicina { get; set; }
        public decimal Cena { get; set; }
        public int? Kolicina { get; set; }
        public int? Popust { get; set; }
        public string Slika { get; set; }

        public virtual ProizvodjacDto Proizvodjac { get; set; }
        public virtual TipProizvodumDto TipProizvoda { get; set; }
        public virtual VrstaProizvodumDto VrstaProizvoda { get; set; }
    }
}
