using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("proizvodjac")]
    public partial class Proizvodjac
    {
        public Proizvodjac()
        {
            Proizvods = new HashSet<Proizvod>();
        }

        [Key]
        [Column("proizvodjac_id")]
        public int ProizvodjacId { get; set; }
        [Required]
        [Column("naziv_proizvodjaca")]
        [StringLength(50)]
        public string NazivProizvodjaca { get; set; }

        [InverseProperty(nameof(Proizvod.Proizvodjac))]
        public virtual ICollection<Proizvod> Proizvods { get; set; }
    }
}
