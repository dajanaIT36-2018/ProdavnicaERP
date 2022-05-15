using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("stavka_porudzbine")]
    public partial class StavkaPorudzbine
    {
        [Key]
        [Column("stavka_porudzbine_id")]
        public int StavkaPorudzbineId { get; set; }
        [Column("proizvod_id")]
        public int ProizvodId { get; set; }
        [Column("porudzbina_id")]
        public int PorudzbinaId { get; set; }
        [Column("kolicina_stavke_porudzbine")]
        public int KolicinaStavkePorudzbine { get; set; }
        [Column("velicina_stavke_porudzbine")]
        public int VelicinaStavkePorudzbine { get; set; }
        [Column("ukupna_cena_stavke_porudzbine", TypeName = "numeric(18, 0)")]
        public decimal UkupnaCenaStavkePorudzbine { get; set; }

        [ForeignKey(nameof(PorudzbinaId))]
        [InverseProperty("StavkaPorudzbines")]
        public virtual Porudzbina Porudzbina { get; set; }
        [ForeignKey(nameof(ProizvodId))]
        [InverseProperty("StavkaPorudzbines")]
        public virtual Proizvod Proizvod { get; set; }
    }
}
