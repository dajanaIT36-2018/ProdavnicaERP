using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("korpa")]
    public partial class Korpa
    {
        public Korpa()
        {
            StavkaKorpes = new HashSet<StavkaKorpe>();
        }

        [Key]
        [Column("korpa_id")]
        public int KorpaId { get; set; }
        [Column("korisnik_id")]
        public int KorisnikId { get; set; }

        [ForeignKey(nameof(KorisnikId))]
        [InverseProperty("Korpas")]
        public virtual Korisnik Korisnik { get; set; }
        [InverseProperty(nameof(StavkaKorpe.Korpa))]
        public virtual ICollection<StavkaKorpe> StavkaKorpes { get; set; }
    }
}
