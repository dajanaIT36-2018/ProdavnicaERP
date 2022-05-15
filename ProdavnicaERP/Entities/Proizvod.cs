using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("proizvod")]
    public partial class Proizvod
    {
        public Proizvod()
        {
            StavkaKorpes = new HashSet<StavkaKorpe>();
            StavkaPorudzbines = new HashSet<StavkaPorudzbine>();
        }

        [Key]
        [Column("proizvod_id")]
        public int ProizvodId { get; set; }
        [Column("tip_proizvoda_id")]
        public int TipProizvodaId { get; set; }
        [Column("vrsta_proizvoda_id")]
        public int VrstaProizvodaId { get; set; }
        [Column("proizvodjac_id")]
        public int ProizvodjacId { get; set; }
        [Column("velicina")]
        public int? Velicina { get; set; }
        [Column("cena", TypeName = "numeric(18, 0)")]
        public decimal Cena { get; set; }
        [Column("kolicina")]
        public int? Kolicina { get; set; }
        [Column("popust")]
        public int? Popust { get; set; }
        [Column("slika")]
        [StringLength(50)]
        public string Slika { get; set; }

        [ForeignKey(nameof(ProizvodjacId))]
        [InverseProperty("Proizvods")]
        public virtual Proizvodjac Proizvodjac { get; set; }
        [ForeignKey(nameof(TipProizvodaId))]
        [InverseProperty(nameof(TipProizvodum.Proizvods))]
        public virtual TipProizvodum TipProizvoda { get; set; }
        [ForeignKey(nameof(VrstaProizvodaId))]
        [InverseProperty(nameof(VrstaProizvodum.Proizvods))]
        public virtual VrstaProizvodum VrstaProizvoda { get; set; }
        [InverseProperty(nameof(StavkaKorpe.Proizvod))]
        public virtual ICollection<StavkaKorpe> StavkaKorpes { get; set; }
        [InverseProperty(nameof(StavkaPorudzbine.Proizvod))]
        public virtual ICollection<StavkaPorudzbine> StavkaPorudzbines { get; set; }
    }
}
