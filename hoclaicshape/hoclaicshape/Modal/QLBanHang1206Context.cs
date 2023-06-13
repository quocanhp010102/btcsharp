using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace hoclaicshape.Modal
{
    public partial class QLBanHang1206Context : DbContext
    {
        public QLBanHang1206Context()
        {
        }

        public QLBanHang1206Context(DbContextOptions<QLBanHang1206Context> options)
            : base(options)
        {
        }

        public virtual DbSet<LoaiSp> LoaiSp { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-R8OV281\\SQLEXPRESS09;Initial Catalog=QLBanHang1206;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.Property(e => e.MaSp)
                    .HasColumnName("MaSP")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.TenSp)
                    .HasColumnName("TenSP")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_SanPham_LoaiSP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
