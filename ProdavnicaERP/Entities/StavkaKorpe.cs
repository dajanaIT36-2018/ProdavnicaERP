using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("stavka_korpe")]
    public partial class StavkaKorpe
    {
        [Key]
        [Column("stavka_korpe_id")]
        public int StavkaKorpeId { get; set; }
        [Column("proizvod_id")]
        public int ProizvodId { get; set; }
        [Column("korpa_id")]
        public int KorpaId { get; set; }
        [Column("kolicina_stavke")]
        public int KolicinaStavke { get; set; }
        [Column("velicina_stavke")]
        public int VelicinaStavke { get; set; }
        [Column("ukupna_cena_stavke", TypeName = "numeric(18, 0)")]
        public decimal UkupnaCenaStavke { get; set; }

        [ForeignKey(nameof(KorpaId))]
        [InverseProperty("StavkaKorpes")]
        public virtual Korpa Korpa { get; set; }
        [ForeignKey(nameof(ProizvodId))]
        [InverseProperty("StavkaKorpes")]
        public virtual Proizvod Proizvod { get; set; }
    }
}
