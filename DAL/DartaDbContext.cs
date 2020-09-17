using DomainModel;
using DomainModel.FieldRekhankanDarta;
using DomainModel.Fiscal;
using DomainModel.HalsabikChalani;
using DomainModel.HalsabikDarta;
using DomainModel.Setting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL {
    public class DartaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int> {
        public DartaDbContext (DbContextOptions<DartaDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.Entity<OfficeName> ().HasOne (p => p.site).WithMany (p => p.Names).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChalanPatras> ().HasOne (p => p.purji)
                .WithMany (p => p.patras).HasForeignKey (p => p.purjiId);

            builder.Entity<ChalanPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<HalsabikChalaniPatras> ().HasOne (p => p.HalsabikChalani)
                .WithMany (p => p.patras).HasForeignKey (p => p.HalsabikChalaniId);

            builder.Entity<HalsabikChalaniPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<HalsabikDartaPatras> ().HasOne (p => p.HalsabikDarta)
                .WithMany (p => p.patras).HasForeignKey (p => p.HalsabikDartaId);

            builder.Entity<HalsabikDartaPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<ChitthiDartaPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<ChitthiDartaPatras> ().HasOne (p => p.purjiDarta)
                .WithMany (p => p.patras).HasForeignKey (p => p.purjiDartaId);

            builder.Entity<RekhankanChalanPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<RekhankanChalanPatras> ().HasOne (p => p.FieldRekhankan)
                .WithMany (p => p.patras).HasForeignKey (p => p.fieldRekhankanChalaniId);

            builder.Entity<RekhankanDartaPatras> ().HasOne (p => p.file)
                .WithMany (p => p.patras).HasForeignKey (p => p.fileId);

            builder.Entity<RekhankanDartaPatras> ().HasOne (p => p.FieldRekhankanDarta)
                .WithMany (p => p.patras).HasForeignKey (p => p.fieldRekhankanDartaId);

            builder.Entity<ChitthiPurji> ().HasOne (p => p.subject).WithMany (p => p.purjiSubject);
           


            builder.Entity<ChitthiPurjiDarta> ().HasOne (p => p.subject).WithMany (p => p.purjiDartaSubject);
            builder.Entity<FieldRekhankanChalani> ().HasOne (p => p.subject).WithMany (p => p.FieldRekhankanChalanis);
            builder.Entity<ChitthiPurji> ().HasOne (p => p.chalan).WithMany (p => p.chalanPurji);
            builder.Entity<FieldRekhankanChalani> ().HasOne (p => p.chalan).WithMany (p => p.FieldRekhankanChalanis);
            builder.Entity<ChitthiPurjiDarta> ().HasOne (p => p.DartaType).WithMany (p => p.PurjiDarta);

            builder.Entity<HalsabikChalani> ().HasOne (p => p.subject).WithMany (p => p.HalsabikChalanis);
            builder.Entity<HalsabikChalani> ().HasOne (p => p.chalan).WithMany (p => p.HalsabikChalanis);

        }

        public DbSet<SiteSetting> siteSettings { get; set; }
        public DbSet<Chalan> chalans { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Dartas> Dartas { get; set; }
        public DbSet<ChitthiPurji> ChitthiPurjis { get; set; }
        public DbSet<HalsabikChalani> HalsabikChalanis { get; set; }
        public DbSet<HalsabikDarta> HalsabikDartas { get; set; }
        public DbSet<FieldRekhankanChalani> fieldRekhankanChalanis { get; set; }
        public DbSet<FieldRekhankanDarta> FieldRekhankanDartas { get; set; }
        public DbSet<ChitthiPurjiDarta> chitthiPurjiDartas { get; set; }
        public DbSet<Prefix> Prefixes { get; set; }
        public virtual DbSet<ChalanFiles> Files { get; set; }
        public virtual DbSet<RekhankanChalanFiles> RekhankanChalanFiles { get; set; }
        public virtual DbSet<FieldRekhankanDartaFile> FieldRekhankanDartaFiles { get; set; }
        public virtual DbSet<PurjiDartaFiles> PurjiDartaFiles { get; set; }
        public virtual DbSet<HalsabikChalaniFile> HalsabikChalaniFiles { get; set; }
        public virtual DbSet<HalsabikDartaFile> HalsabikDartaFiles { get; set; }

        public virtual DbSet<FiscalYear> FiscalYears {get;set;}
        //  public virtual DbSet<ChalanPatras> patras { get; set; }
    }
}