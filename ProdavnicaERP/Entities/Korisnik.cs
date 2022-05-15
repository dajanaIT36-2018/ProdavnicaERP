using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProdavnicaERP.Entities
{
    [Table("korisnik")]
    public partial class Korisnik
    {
        public Korisnik()
        {
            Korpas = new HashSet<Korpa>();
            Porudzbinas = new HashSet<Porudzbina>();
        }

        [Key]
        [Column("korisnik_id")]
        public int KorisnikId { get; set; }
        [Column("ime_korisnik")]
        [StringLength(50)]
        public string ImeKorisnik { get; set; }
        [Column("prezime_korisnika")]
        [StringLength(50)]
        public string PrezimeKorisnika { get; set; }
        [Column("telefon_korisnika")]
        public int? TelefonKorisnika { get; set; }
        [Column("eMail_korisnika")]
        [StringLength(50)]
        public string EMailKorisnika { get; set; }
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
        [Column("pol")]
        [StringLength(8)]
        public string Pol { get; set; }
        [Required]
        [Column("korisnicko_ime")]
        [StringLength(50)]
        public string KorisnickoIme { get; set; }
        [Required]
        [Column("lozinka")]
        [StringLength(50)]
        public string Lozinka { get; set; }
        [Column("tip_korisnika_id")]
        public int TipKorisnikaId { get; set; }

        [ForeignKey(nameof(TipKorisnikaId))]
        [InverseProperty("Korisniks")]
        public virtual TipKorisnika TipKorisnika { get; set; }
        [InverseProperty(nameof(Korpa.Korisnik))]
        public virtual ICollection<Korpa> Korpas { get; set; }
        [InverseProperty(nameof(Porudzbina.Korisnik))]
        public virtual ICollection<Porudzbina> Porudzbinas { get; set; }

        [NotMapped]
        public string Rola { get; set; }
    }
}
