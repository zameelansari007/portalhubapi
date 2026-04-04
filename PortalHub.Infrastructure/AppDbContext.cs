using Microsoft.EntityFrameworkCore;
using PortalHub.Domain.Models.Master;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Master Schema
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<PaymentStatus> PaymentStatuses => Set<PaymentStatus>();

        // Portal Schema
        public DbSet<User> Users => Set<User>();
        public DbSet<UserPassword> UserPasswords => Set<UserPassword>();
        public DbSet<UserSocialAccount> UserSocialAccounts => Set<UserSocialAccount>();
        public DbSet<UserOtp> UserOtps => Set<UserOtp>();
        public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
        public DbSet<AuthAuditLog> AuthAuditLogs => Set<AuthAuditLog>();
        public DbSet<BlockedIp> BlockedIps => Set<BlockedIp>();
        
        public DbSet<SubscriptionOrder> SubscriptionOrders => Set<SubscriptionOrder>();
        public DbSet<SubscriptionPayment> SubscriptionPayments => Set<SubscriptionPayment>();
        public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();
        public DbSet<SupplierProfile> SupplierProfiles => Set<SupplierProfile>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= MASTER =================
            modelBuilder.Entity<Role>()
                .ToTable("Roles", "Master")
                .HasKey(x => x.RoleId);

            modelBuilder.Entity<SubscriptionPlan>()
                .ToTable("SubscriptionPlans", "Master")
                .HasKey(x => x.PlanId);

            modelBuilder.Entity<PaymentStatus>()
                .ToTable("PaymentStatus", "Master")
                .HasKey(x => x.PaymentStatusId);

           //======================Auth===================

           modelBuilder.Entity<AuthAuditLog>()
    .ToTable("AuthAuditLogs", "Portal")
    .HasKey(x => x.AuditLogId);

modelBuilder.Entity<AuthAuditLog>()
    .HasOne(x => x.User)
    .WithMany()
    .HasForeignKey(x => x.UserId)
    .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<BlockedIp>()
    .ToTable("BlockedIps", "Portal")
    .HasKey(x => x.Id);

modelBuilder.Entity<BlockedIp>()
    .Property(x => x.IpAddress)
    .HasMaxLength(45)
    .IsRequired();

modelBuilder.Entity<BlockedIp>()
    .Property(x => x.Reason)
    .HasMaxLength(200);


            // ================= PORTAL =================
            modelBuilder.Entity<User>()
                .ToTable("Users", "Portal")
                .HasKey(x => x.UserId);

            modelBuilder.Entity<UserPassword>()
                .ToTable("UserPasswords", "Portal")
                .HasKey(x => x.UserId); // 1-1 PK

            modelBuilder.Entity<UserSocialAccount>()
                .ToTable("UserSocialAccounts", "Portal")
                .HasKey(x => x.SocialId);

            modelBuilder.Entity<UserOtp>()
                .ToTable("UserOtps", "Portal")
                .HasKey(x => x.OtpId);

            modelBuilder.Entity<UserRefreshToken>()
                .ToTable("UserRefreshTokens", "Portal")
                .HasKey(x => x.RefreshTokenId);

            modelBuilder.Entity<SubscriptionOrder>()
                .ToTable("SubscriptionOrders", "Portal")
                .HasKey(x => x.SubscriptionOrderId);

            modelBuilder.Entity<SubscriptionPayment>()
                .ToTable("SubscriptionPayments", "Portal")
                .HasKey(x => x.SubscriptionPaymentId);

            modelBuilder.Entity<UserSubscription>()
                .ToTable("UserSubscriptions", "Portal")
                .HasKey(x => x.SubscriptionId);

            modelBuilder.Entity<SupplierProfile>()
                .ToTable("SupplierProfiles", "Portal")
                .HasKey(x => x.SupplierId);

            // ================= CATEGORIES =================
            modelBuilder.Entity<Category>()
                .ToTable("Categories", "Portal")
                .HasKey(x => x.CategoryId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .Property(c => c.SlugPath)
                .HasMaxLength(450);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.SlugPath)
                .IsUnique();

            // ================= PRODUCTS =================
            modelBuilder.Entity<Product>()
                .ToTable("Products", "Portal")
                .HasKey(x => x.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId);

            // ================= PRODUCT VARIANTS =================
            modelBuilder.Entity<ProductVariant>()
                .ToTable("ProductVariants", "Portal")
                .HasKey(x => x.ProductVariantId);

            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => v.Sku)
                .IsUnique();

            // ================= PRODUCT IMAGES =================
            modelBuilder.Entity<ProductImage>()
                .ToTable("ProductImages", "Portal")
                .HasKey(x => x.ProductImageId);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImage>()
                .Property(pi => pi.ImageUrl)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<ProductImage>()
                .HasIndex(pi => pi.ProductId);


            // ================= RELATIONSHIPS =================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Password)
                .WithOne(p => p.User)
                .HasForeignKey<UserPassword>(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.SupplierProfile)
                .WithOne(s => s.User)
                .HasForeignKey<SupplierProfile>(s => s.SupplierId);

            modelBuilder.Entity<SubscriptionOrder>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<SubscriptionOrder>()
                .HasOne(o => o.Plan)
                .WithMany()
                .HasForeignKey(o => o.PlanId);

            modelBuilder.Entity<SubscriptionOrder>()
                .HasOne(o => o.PaymentStatus)
                .WithMany()
                .HasForeignKey(o => o.PaymentStatusId);

            modelBuilder.Entity<SubscriptionPayment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.SubscriptionOrderId);

            modelBuilder.Entity<SubscriptionPayment>()
                .HasOne(p => p.PaymentStatus)
                .WithMany()
                .HasForeignKey(p => p.PaymentStatusId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Plan)
                .WithMany()
                .HasForeignKey(us => us.PlanId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Order)
                .WithMany()
                .HasForeignKey(us => us.SubscriptionOrderId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.PaymentStatus)
                .WithMany()
                .HasForeignKey(us => us.PaymentStatusId);
        }
    }
}
