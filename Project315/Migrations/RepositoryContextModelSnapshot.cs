﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Project315.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Entities.Models.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("CategoriaId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("categoria");
                });

            modelBuilder.Entity("Entities.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("PedidoId");

                    b.Property<Guid>("ProductoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ShoppyCarId")
                        .HasColumnType("char(36)");

                    b.Property<ulong>("cantidad")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("ShoppyCarId");

                    b.ToTable("pedido");
                });

            modelBuilder.Entity("Entities.Models.Producto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("ProductoId");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("producto");
                });

            modelBuilder.Entity("Entities.Models.ShoppyCar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("ShoppyCarId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("shoppycar");
                });

            modelBuilder.Entity("Entities.Models.Pedido", b =>
                {
                    b.HasOne("Entities.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.ShoppyCar", null)
                        .WithMany("Pedidos")
                        .HasForeignKey("ShoppyCarId");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Entities.Models.Producto", b =>
                {
                    b.HasOne("Entities.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("Entities.Models.ShoppyCar", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
