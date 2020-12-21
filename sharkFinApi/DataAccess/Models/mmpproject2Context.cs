using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models
{
    public partial class mmpproject2Context : DbContext
    {

        public mmpproject2Context(DbContextOptions<mmpproject2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<PortfolioEntry> PortfolioEntries { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

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
                    .HasConstraintName("FK__Portfolio__UserI__6EF57B66");
            });

            modelBuilder.Entity<PortfolioEntry>(entity =>
            {
                entity.HasKey(e => new { e.PortfolioId, e.StockSymbol, e.StockMarket })
                    .HasName("PK__Portfoli__29B9ADE3DD119CFA");

                entity.ToTable("PortfolioEntry");

                entity.Property(e => e.StockSymbol).HasMaxLength(99);

                entity.Property(e => e.StockMarket).HasMaxLength(99);

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.PortfolioEntries)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Portfolio__Portf__72C60C4A");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.PortfolioEntries)
                    .HasForeignKey(d => new { d.StockSymbol, d.StockMarket })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PortfolioEntry__73BA3083");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new { e.Symbol, e.Market })
                    .HasName("PK__Stocks__132F00CFB656555F");

                entity.Property(e => e.Symbol).HasMaxLength(99);

                entity.Property(e => e.Market).HasMaxLength(99);

                entity.Property(e => e.Logo).HasMaxLength(99);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StockMarket)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.StockSymbol)
                    .IsRequired()
                    .HasMaxLength(99);

                entity.Property(e => e.TimeTraded).HasColumnType("datetime");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trades__Portfoli__76969D2E");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => new { d.StockSymbol, d.StockMarket })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trades__778AC167");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A1B3ABC1")
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
