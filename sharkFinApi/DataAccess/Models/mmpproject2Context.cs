using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models {
    public partial class mmpproject2Context : DbContext
    {
        public mmpproject2Context()
        {
        }

        public mmpproject2Context(DbContextOptions<mmpproject2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasIndex(e => new { e.PortfolioId, e.StockId }, "AK_AssetPortfolioIdStockId")
                    .IsUnique();

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Assets__Portfoli__45BE5BA9");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Assets__StockId__46B27FE2");
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.Property(e => e.Funds)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((500.00))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Portfolio__UserI__40F9A68C");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasIndex(e => new { e.Symbol, e.Market }, "AK_StockSymbolMarket")
                    .IsUnique();

                entity.Property(e => e.Logo).HasMaxLength(99);

                entity.Property(e => e.Market)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TimeTraded).HasColumnType("datetime");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trades__Portfoli__498EEC8D");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trades__StockId__4A8310C6");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D105343BA3AE6E")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(99);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
