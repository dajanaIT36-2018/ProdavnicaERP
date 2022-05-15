using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("vrsta_proizvoda")]
    public partial class VrstaProizvodum
    {
        public VrstaProizvodum()
        {
            Proizvods = new HashSet<Proizvod>();
        }

        [Key]
        [Column("vrsta_proizvoda_id")]
        public int VrstaProizvodaId { get; set; }
        [Required]
        [Column("naziv_vrste_proizvoda")]
        [StringLength(50)]
        public string NazivVrsteProizvoda { get; set; }

        [InverseProperty(nameof(Proizvod.VrstaProizvoda))]
        public virtual ICollection<Proizvod> Proizvods { get; set; }
    }
}
