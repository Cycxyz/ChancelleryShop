using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ChancelleryShop
{
    public partial class ChancelleryContext : DbContext
    {
        public ChancelleryContext()
        {
            Database.EnsureCreated();
        }

        public ChancelleryContext(DbContextOptions<ChancelleryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductToReceipt> ProductToReceipts { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=VLADM23\\CYCXYZSERVER;Database=Chancellery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E03BA69906")
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductName, "UQ__Products__DD5A978A85C9D5DF")
                    .IsUnique();

                entity.Property(e => e.ProductImage).IsRequired();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__3A81B327");
            });

            modelBuilder.Entity<ProductToReceipt>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductToReceipts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductTo__Produ__403A8C7D");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ProductToReceipts)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductTo__Recei__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
