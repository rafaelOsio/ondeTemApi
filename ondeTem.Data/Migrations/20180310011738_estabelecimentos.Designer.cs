﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using ondeTem.Data.Context;
using System;

namespace ondeTem.Data.Migrations
{
    [DbContext(typeof(OndeTemContext))]
    [Migration("20180310011738_estabelecimentos")]
    partial class estabelecimentos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("ondeTem.Domain.EstabelecimentoRoot.Estabelecimento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<bool?>("Desativado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(false);

                    b.Property<decimal?>("Latitude")
                        .IsRequired();

                    b.Property<decimal?>("Longitude")
                        .IsRequired();

                    b.Property<string>("MensagemParaClientes")
                        .HasColumnType("varchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<string>("TelefonePrincipal")
                        .IsRequired()
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("TelefoneSecundario")
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("Id");

                    b.ToTable("estabelecimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
