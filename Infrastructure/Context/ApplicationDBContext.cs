using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Entities.Identity;
using Data.Entities.Child;
using System.Reflection;
using Infrastructure.Configurations;
namespace Infrastructure.Context
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            _encryptionProvider=new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        public DbSet<User> User { get; set; }
        public DbSet<Student> students { get; set; }
       public DbSet<ChildProfile> ChildProfiles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

        #region Views
        // public DbSet<ViewDepartment> ViewDepartment { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ChildConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
