using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;

namespace Palmfit.Data.AppDbContext
{
    public class PalmfitDbContext : IdentityDbContext<AppUser, AppUserRole, string>
    {
        public DbSet<Health> Healths { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<FoodClass> FoodClasses { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<UserOTP> UserOTPs { get; set; }
        public DbSet<AppUserPermission> AppUserPermissions { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<FileUploadModel> FileUploadmodels { get; set; }
        public DbSet<SelectedPlans> SelectedPlans { get; set; }
		 
        public DbSet<CalorieData> userCalorieTable { get; set; }
        public DbSet<AllCalorieData> AllCalorieInfos { get; set; } 

        public PalmfitDbContext(DbContextOptions<PalmfitDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure IdentityUserRole<string> as a keyless entity type
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            // Configure One AppUser to Zero or One Relationships
            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.Health)
                .WithOne(h => h.AppUser)
                .HasForeignKey<Health>(h => h.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.Setting)
                .WithOne(s => s.AppUser)
                .HasForeignKey<Setting>(s => s.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.Wallet)
                .WithOne(w => w.AppUser)
                .HasForeignKey<Wallet>(w => w.AppUserId);

            //Configure One AppUser to Many Relationships
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Invities)
                .WithOne(i => i.AppUser)
                .HasForeignKey(i => i.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Notifications)
                .WithOne(n => n.AppUser)
                .HasForeignKey(n => n.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.AppUser)
                .HasForeignKey(r => r.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Subscriptions)
                .WithOne(s => s.AppUser)
                .HasForeignKey(s => s.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId);

            // Configure One to One Health Relationship
            modelBuilder.Entity<Health>()
                .HasOne(h => h.AppUser)
                .WithOne(a => a.Health)
                .HasForeignKey<Health>(h => h.AppUserId);

            // Configure One to Zero or One Wallet Relationship
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.AppUser)
                .WithOne(a => a.Wallet)
                .HasForeignKey<Wallet>(w => w.AppUserId);

            // Configure One Wallet to Many Relationships
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.AppUser);


            // Configure One to Many Transaction Relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.AppUser)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AppUserId);

            // Configure One to Many Subscription Relationship
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.AppUser)
                .WithMany(a => a.Subscriptions)
                .HasForeignKey(s => s.AppUserId);

            // Configure One to Many Reviews Relationships
            modelBuilder.Entity<Review>()
                .HasOne(r => r.AppUser)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.AppUserId);

            // Configure One to Many Notification Relationship
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.AppUser)
                .WithMany(a => a.Notifications)
                .HasForeignKey(n => n.AppUserId);

            //Configure One to Many Invite Relationship
            modelBuilder.Entity<Invite>()
                .HasOne(i => i.AppUser)
                .WithMany(a => a.Invities)
                .HasForeignKey(i => i.AppUserId);

			// Configure One FoodClass to Many Relationship
			modelBuilder.Entity<FoodClass>()
				.HasMany(fc => fc.MealPlan) // Use the 'MealPlan' property of FoodClass
				.WithOne(mp => mp.FoodClass)
				.HasForeignKey(mp => mp.FoodClassId);

			/* <-------Start-------- Configure Enum Mapping in DbContext ------- Start------>*/
			modelBuilder.Entity<Food>()
                .Property(f => f.Unit)
                .HasConversion<string>();

            /* <-------End-------- Configure Enum Mapping in DbContext ------- End------>*/
        }
    }

    /* <-------Start-------- Seed Data ------- Start------>*/
    // Seed FoodClass data
    public class SeedData
    {
        public static void Initialize(PalmfitDbContext context)
        {
        }
    }
    /* <-------End-------- Seed Data ------- End------>*/
}