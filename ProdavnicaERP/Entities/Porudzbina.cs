using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("porudzbina")]
    public partial class Porudzbina
    {
        public Porudzbina()
        {
            StavkaPorudzbines = new HashSet<StavkaPorudzbine>();
        }

        [Key]
        [Column("porudzbina_id")]
        public int PorudzbinaId { get; set; }
        [Column("status_porudzbine_id")]
        public int StatusPorudzbineId { get; set; }
        [Column("korisnik_id")]
        public int KorisnikId { get; set; }
        [Column("grad_korisnika")]
        [StringLength(50)]
        public string GradKorisnika { get; set; }
        [Column("ulica_korisnika")]
        [StringLength(50)]
        public string UlicaKorisnika { get; set; }
        [Column("brUlice_korisnika")]
        [StringLength(50)]
        public string BrUliceKorisnika { get; set; }
        [Column("postanski_broj", TypeName = "numeric(18, 0)")]
        public decimal? PostanskiBroj { get; set; }
        [Column("iznos_dostave", TypeName = "numeric(18, 0)")]
        public decimal IznosDostave { get; set; }
        [Column("ukupanIznos_porudzbine", TypeName = "numeric(18, 0)")]
        public decimal UkupanIznosPorudzbine { get; set; }
        [Required]
        [Column("nacin_placanja")]
        [StringLength(20)]
        public string NacinPlacanja { get; set; }
        [Column("datum", TypeName = "date")]
        public DateTime? Datum { get; set; }

        [ForeignKey(nameof(KorisnikId))]
        [InverseProperty("Porudzbinas")]
        public virtual Korisnik Korisnik { get; set; }
        [ForeignKey(nameof(StatusPorudzbineId))]
        [InverseProperty("Porudzbinas")]
        public virtual StatusPorudzbine StatusPorudzbine { get; set; }
        [InverseProperty(nameof(StavkaPorudzbine.Porudzbina))]
        public virtual ICollection<StavkaPorudzbine> StavkaPorudzbines { get; set; }
    }
}
