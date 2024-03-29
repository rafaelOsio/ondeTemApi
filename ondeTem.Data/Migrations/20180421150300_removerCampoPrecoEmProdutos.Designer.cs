﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ondeTem.Data.Context;
using System;

namespace ondeTem.Data.Migrations
{
    [DbContext(typeof(OndeTemContext))]
    [Migration("20180421150300_removerCampoPrecoEmProdutos")]
    partial class removerCampoPrecoEmProdutos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("ondeTem.Domain.CategoriaRoot.Categoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("ondeTem.Domain.EstabelecimentoRoot.Estabelecimento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<string>("CaminhoImage")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(65)")
                        .HasMaxLength(65);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

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

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("estabelecimentos");
                });

            modelBuilder.Entity("ondeTem.Domain.ProdutoRoot.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Acessos");

                    b.Property<string>("CaminhoImage")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<long>("CategoriaId");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<long>("EstabelecimentoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("EstabelecimentoId");

                    b.ToTable("produtos");
                });

            modelBuilder.Entity("ondeTem.Domain.UserRoot.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool?>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("ondeTem.Domain.EstabelecimentoRoot.Estabelecimento", b =>
                {
                    b.HasOne("ondeTem.Domain.UserRoot.User", "User")
                        .WithMany("Estabelecimentos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ondeTem.Domain.ProdutoRoot.Produto", b =>
                {
                    b.HasOne("ondeTem.Domain.CategoriaRoot.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ondeTem.Domain.EstabelecimentoRoot.Estabelecimento", "Estabelecimento")
                        .WithMany("Produtos")
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
