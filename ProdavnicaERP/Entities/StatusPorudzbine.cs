using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("status_porudzbine")]
    public partial class StatusPorudzbine
    {
        public StatusPorudzbine()
        {
            Porudzbinas = new HashSet<Porudzbina>();
        }

        [Key]
        [Column("status_porudzbine_id")]
        public int StatusPorudzbineId { get; set; }
        [Column("naziv_statusa_porudzbine")]
        [StringLength(20)]
        public string NazivStatusaPorudzbine { get; set; }

        [InverseProperty(nameof(Porudzbina.StatusPorudzbine))]
        public virtual ICollection<Porudzbina> Porudzbinas { get; set; }
    }
}
