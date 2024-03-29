﻿using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.API.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NSE.Carrinho.API.Data
{
    public class CarrinhoContext : DbContext
    {
        public CarrinhoContext(DbContextOptions<CarrinhoContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<CarrinhoCliente> CarrinhoCliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CarrinhoCliente>().HasIndex(c => c.ClienteId).HasName("IDX_Cliente");
            modelBuilder.Entity<CarrinhoCliente>().HasMany(c => c.Itens).WithOne(i => i.CarrinhoCliente).HasForeignKey(fk => fk.CarrinhoId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
