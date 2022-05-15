using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("tip_proizvoda")]
    public partial class TipProizvodum
    {
        public TipProizvodum()
        {
            Proizvods = new HashSet<Proizvod>();
        }

        [Key]
        [Column("tip_proizvoda_id")]
        public int TipProizvodaId { get; set; }
        [Required]
        [Column("naziv_tip_proizvoda")]
        [StringLength(50)]
        public string NazivTipProizvoda { get; set; }

        [InverseProperty(nameof(Proizvod.TipProizvoda))]
        public virtual ICollection<Proizvod> Proizvods { get; set; }
    }
}
