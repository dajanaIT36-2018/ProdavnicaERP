using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("tip_korisnika")]
    public partial class TipKorisnika
    {
        public TipKorisnika()
        {
            Korisniks = new HashSet<Korisnik>();
        }

        [Key]
        [Column("tip_korisnika_id")]
        public int TipKorisnikaId { get; set; }
        [Column("tip")]
        [StringLength(15)]
        public string Tip { get; set; }

        [InverseProperty(nameof(Korisnik.TipKorisnika))]
        public virtual ICollection<Korisnik> Korisniks { get; set; }
    }
}
