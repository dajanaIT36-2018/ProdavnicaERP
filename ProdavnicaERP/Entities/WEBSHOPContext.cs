using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProdavnicaERP.Entities
{
    public partial class WEBSHOPContext : DbContext
    {
        //public WEBSHOPContext()
        //{
        //}

        public WEBSHOPContext(DbContextOptions<WEBSHOPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Korisnik> Korisniks { get; set; }
        public virtual DbSet<Korpa> Korpas { get; set; }
        public virtual DbSet<Porudzbina> Porudzbinas { get; set; }
        public virtual DbSet<Proizvod> Proizvods { get; set; }
        public virtual DbSet<Proizvodjac> Proizvodjacs { get; set; }
        public virtual DbSet<StatusPorudzbine> StatusPorudzbines { get; set; }
        public virtual DbSet<StavkaKorpe> StavkaKorpes { get; set; }
        public virtual DbSet<StavkaPorudzbine> StavkaPorudzbines { get; set; }
        public virtual DbSet<TipKorisnika> TipKorisnikas { get; set; }
        public virtual DbSet<TipProizvodum> TipProizvoda { get; set; }
        public virtual DbSet<VrstaProizvodum> VrstaProizvoda { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:WEBSHOP");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.KorisnikId).HasDefaultValueSql("(NEXT VALUE FOR [IDkorisnik])");

                entity.Property(e => e.BrUliceKorisnika).IsUnicode(false);

                entity.Property(e => e.EMailKorisnika).IsUnicode(false);

                entity.Property(e => e.GradKorisnika).IsUnicode(false);

                entity.Property(e => e.ImeKorisnik).IsUnicode(false);

                entity.Property(e => e.KorisnickoIme).IsUnicode(false);

                entity.Property(e => e.Lozinka).IsUnicode(false);

                entity.Property(e => e.Pol).IsUnicode(false);

                entity.Property(e => e.PrezimeKorisnika).IsUnicode(false);

                entity.Property(e => e.UlicaKorisnika).IsUnicode(false);

                entity.HasOne(d => d.TipKorisnika)
                    .WithMany(p => p.Korisniks)
                    .HasForeignKey(d => d.TipKorisnikaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_korisnik_tip_korisnika");
            });

            modelBuilder.Entity<Korpa>(entity =>
            {
                entity.Property(e => e.KorpaId).HasDefaultValueSql("(NEXT VALUE FOR [IDkorpa])");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Korpas)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_korpa_korisnik");
            });

            modelBuilder.Entity<Porudzbina>(entity =>
            {
                entity.Property(e => e.PorudzbinaId).HasDefaultValueSql("(NEXT VALUE FOR [IDporudzbina])");

                entity.Property(e => e.BrUliceKorisnika).IsUnicode(false);

                entity.Property(e => e.GradKorisnika).IsUnicode(false);

                entity.Property(e => e.NacinPlacanja).IsUnicode(false);

                entity.Property(e => e.UlicaKorisnika).IsUnicode(false);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Porudzbinas)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_porudzbina_korisnik");

                entity.HasOne(d => d.StatusPorudzbine)
                    .WithMany(p => p.Porudzbinas)
                    .HasForeignKey(d => d.StatusPorudzbineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_porudzbina_status_porudzine");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.Property(e => e.ProizvodId).HasDefaultValueSql("(NEXT VALUE FOR [IDproizvod])");

                entity.Property(e => e.Slika).IsUnicode(false);

                entity.HasOne(d => d.Proizvodjac)
                    .WithMany(p => p.Proizvods)
                    .HasForeignKey(d => d.ProizvodjacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proizvod_proizvodjac");

                entity.HasOne(d => d.TipProizvoda)
                    .WithMany(p => p.Proizvods)
                    .HasForeignKey(d => d.TipProizvodaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proizvod_tip_proizvoda");

                entity.HasOne(d => d.VrstaProizvoda)
                    .WithMany(p => p.Proizvods)
                    .HasForeignKey(d => d.VrstaProizvodaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proizvod_vrsta_proizvoda");
            });

            modelBuilder.Entity<Proizvodjac>(entity =>
            {
                entity.Property(e => e.ProizvodjacId).HasDefaultValueSql("(NEXT VALUE FOR [IDproizvodjac])");

                entity.Property(e => e.NazivProizvodjaca).IsUnicode(false);
            });

            modelBuilder.Entity<StatusPorudzbine>(entity =>
            {
                entity.Property(e => e.StatusPorudzbineId).HasDefaultValueSql("(NEXT VALUE FOR [IDstatus_porudzbine])");

                entity.Property(e => e.NazivStatusaPorudzbine).IsUnicode(false);
            });

            modelBuilder.Entity<StavkaKorpe>(entity =>
            {
                entity.Property(e => e.StavkaKorpeId).HasDefaultValueSql("(NEXT VALUE FOR [IDstavka_korpe])");

                entity.HasOne(d => d.Korpa)
                    .WithMany(p => p.StavkaKorpes)
                    .HasForeignKey(d => d.KorpaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stavka_korpe_korpa");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.StavkaKorpes)
                    .HasForeignKey(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stavka_korpe_proizvod");
            });

            modelBuilder.Entity<StavkaPorudzbine>(entity =>
            {
                entity.Property(e => e.StavkaPorudzbineId).HasDefaultValueSql("(NEXT VALUE FOR [IDstavka_porudzbine])");

                entity.HasOne(d => d.Porudzbina)
                    .WithMany(p => p.StavkaPorudzbines)
                    .HasForeignKey(d => d.PorudzbinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stavka_porudzbine_korisnik");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.StavkaPorudzbines)
                    .HasForeignKey(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stavka_porudzbine_proizvod");
            });

            modelBuilder.Entity<TipKorisnika>(entity =>
            {
                entity.Property(e => e.TipKorisnikaId).HasDefaultValueSql("(NEXT VALUE FOR [IDtip_korisnika])");

                entity.Property(e => e.Tip).IsUnicode(false);
            });

            modelBuilder.Entity<TipProizvodum>(entity =>
            {
                entity.HasKey(e => e.TipProizvodaId)
                    .HasName("PK_tip_proizvoda_id");

                entity.Property(e => e.TipProizvodaId).HasDefaultValueSql("(NEXT VALUE FOR [IDtipProizvoda])");

                entity.Property(e => e.NazivTipProizvoda).IsUnicode(false);
            });

            modelBuilder.Entity<VrstaProizvodum>(entity =>
            {
                entity.HasKey(e => e.VrstaProizvodaId)
                    .HasName("PK_vrsta_proizvoda_id");

                entity.Property(e => e.VrstaProizvodaId).HasDefaultValueSql("(NEXT VALUE FOR [IDvrstaProizvoda])");

                entity.Property(e => e.NazivVrsteProizvoda).IsUnicode(false);
            });

            modelBuilder.HasSequence<decimal>("IDglavni_admin")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDkorisnik")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDkorpa")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDkupac")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDporudzbina")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDporudzina")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDproizvod")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDproizvodjac")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDstatus_porudzbine")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDstavka_korpe")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDstavka_porudzbine")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDtip_korisnika")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDtipProizvoda")
                .HasMin(1)
                .HasMax(99999999);

            modelBuilder.HasSequence<decimal>("IDvrstaProizvoda")
                .HasMin(1)
                .HasMax(99999999);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
