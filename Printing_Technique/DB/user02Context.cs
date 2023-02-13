using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Printing_Technique.Models;

namespace Printing_Technique.DB
{
    public partial class user02Context : DbContext
    {
        public user02Context()
        {
        }

        public user02Context(DbContextOptions<user02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Consumable> Consumables { get; set; } = null!;
        public virtual DbSet<CrossConsTech> CrossConsTeches { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Technic> Technics { get; set; } = null!;
        public virtual DbSet<Сabinet> Сabinets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.200.35;database=user02;user=user02;password=77053");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumable>(entity =>
            {
                entity.ToTable("Consumable");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrossConsTech>(entity =>
            {
                entity.ToTable("CrossConsTech");

                entity.HasOne(d => d.IdConsNavigation)
                    .WithMany(p => p.CrossConsTeches)
                    .HasForeignKey(d => d.IdCons)
                    .HasConstraintName("FK_CrossConsTech_Consumable");

                entity.HasOne(d => d.IdTechNavigation)
                    .WithMany(p => p.CrossConsTeches)
                    .HasForeignKey(d => d.IdTech)
                    .HasConstraintName("FK_CrossConsTech_Technics");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.IdCross).HasColumnName("ID_cross");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdCrossNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdCross)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_CrossConsTech");
            });

            modelBuilder.Entity<Technic>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.IdCabinet).HasColumnName("ID_Cabinet");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.IdCabinetNavigation)
                    .WithMany(p => p.Technics)
                    .HasForeignKey(d => d.IdCabinet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technics_Сabinet");
            });

            modelBuilder.Entity<Сabinet>(entity =>
            {
                entity.ToTable("Сabinet");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdDepartment).HasColumnName("ID_Department");

                entity.Property(e => e.Namber)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Сabinets)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_Сabinet_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
