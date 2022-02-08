using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Data
{
    public class FinalContext : DbContext
    {
        public FinalContext() { }
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.User> User { get; set; }

        public DbSet<Final.Models.Log> Log { get; set; }

        public DbSet<Final.Models.Category> Category { get; set; }

        public DbSet<Final.Models.Product> Product { get; set; }

        public DbSet<Final.Models.Purchase> Purchase { get; set; }

        public DbSet<Final.Models.Cart> Cart { get; set; }

        public DbSet<Final.Models.Coupon> Coupon { get; set; }

        public DbSet<Final.Models.CartPurchase> CartPurchase { get; set; }

        public DbSet<Final.Models.CartProduct> CartProduct { get; set; }

        public DbSet<Final.Models.CartProduct> PurchaseCoupon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8B2G46C\SQLEXPRESS;Initial Catalog=betomarket_tp4;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table names
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users");
                user.HasKey(u => u.userId);
                user.HasOne(u => u.cart).WithOne(c => c.user).HasForeignKey<Cart>(c => c.userId);
            });

            modelBuilder.Entity<Product>(prod =>
            {
                prod.ToTable("Products");
            });

            modelBuilder.Entity<Category>(cat =>
            {
                cat.ToTable("Categories");
            });

            modelBuilder.Entity<Purchase>(purchase =>
            {
                purchase.ToTable("Purchases");
                purchase.HasKey(p => p.purchaseId);
            });

            modelBuilder.Entity<Cart>(cart =>
            {
                cart.ToTable("Cart");
                cart.HasKey(c => c.cartId);
                cart.HasOne(c => c.user).WithOne(u => u.cart).HasForeignKey<Cart>(c => c.userId);
            });

            modelBuilder.Entity<Log>(log =>
            {
                log.ToTable("Logs");
                log.HasKey(l => l.logId);
            });

            modelBuilder.Entity<Coupon>(cp =>
            {
                cp.ToTable("Coupons");
            });

            //data properties
            modelBuilder.Entity<User>(
                usr =>
                {
                    usr.Property(u => u.dni).IsRequired(true);
                    usr.Property(u => u.CUITCUIL).IsRequired(true);
                    usr.Property(u => u.name).IsRequired(true);
                    usr.Property(u => u.email).IsRequired(true);
                    usr.Property(u => u.password).IsRequired(true);
                    usr.Property(u => u.userType).IsRequired(true);
                    usr.Property(u => u.locked).HasDefaultValue(false);
                });


            modelBuilder.Entity<Product>(
                prod =>
                {
                    prod.Property(p => p.productId).IsRequired(true);
                    prod.Property(p => p.name).IsRequired(true);
                    prod.Property(p => p.price).IsRequired(true);
                    prod.Property(p => p.ammount).IsRequired(true);
                    prod.Property(p => p.description).IsRequired(true);
                });


            modelBuilder.Entity<Coupon>(
                cpn =>
                {
                    cpn.Property(c => c.id).IsRequired(true);
                    cpn.Property(c => c.code).IsRequired(true);
                    cpn.Property(c => c.percentage).IsRequired(true);
                });

            modelBuilder.Entity<Log>(
                log =>
                {
                    log.Property(l => l.logId).IsRequired(true);
                    log.Property(l => l.eventType).IsRequired(true);
                    log.Property(l => l.createdAt).IsRequired(true);
                }
             );

            modelBuilder.Entity<Category>(
                cat =>
                {
                    cat.Property(c => c.categoryId).IsRequired(true);
                    cat.Property(c => c.name).IsRequired(true);
                });

            modelBuilder.Entity<Purchase>(
                prch =>
                {
                    prch.Property(p => p.purchaseId).IsRequired(true);
                    prch.Property(p => p.total).IsRequired(true);
                });

            modelBuilder.Entity<Cart>(
               carro =>
               {
                   carro.Property(c => c.cartId).IsRequired(true);
               });

            //MANY TO MANY Purchase <-> Product
            modelBuilder.Entity<Purchase>()
                .HasMany(P => P.products)
                .WithMany(P => P.Purchases)
                .UsingEntity<CartPurchase>(
                    pp => pp.HasOne(cp => cp.product).WithMany(p => p.CartPurchases).HasForeignKey(pp => pp.productId),
                    pp => pp.HasOne(cp => cp.purchase).WithMany(c => c.cartPurchases).HasForeignKey(pp => pp.purchaseId),
                    pp => pp.HasKey(k => new { k.purchaseId, k.productId })
            );

            //MANY TO MANY Purchase <-> Coupon
            modelBuilder.Entity<Purchase>()
                .HasMany(P => P.coupons)
                .WithMany(C => C.purchases)
                .UsingEntity<PurchaseCoupon>(
                    pp => pp.HasOne(pc => pc.coupon).WithMany(c => c.purchaseCoupon).HasForeignKey(pc => pc.couponId),
                    pp => pp.HasOne(cp => cp.purchase).WithMany(p => p.purchaseCoupon).HasForeignKey(pp => pp.purchaseId),
                    pp => pp.HasKey(k => new { k.purchaseId, k.couponId })
            );

            //MANY TO MANY Cart <-> Product
            modelBuilder.Entity<Cart>()
                .HasMany(C => C.products)
                .WithMany(P => P.Carts)
                .UsingEntity<CartProduct>(
                    rcp => rcp.HasOne(cp => cp.product).WithMany(p => p.CartProducts).HasForeignKey(rcp => rcp.productId),
                    rcp => rcp.HasOne(cp => cp.cart).WithMany(c => c.CartProducts).HasForeignKey(rcp => rcp.cartId),
                    rcp => rcp.HasKey(k => new { k.cartId, k.productId })
            );
            
        }
    }
}


