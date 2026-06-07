using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data.Entities.AbilitiesTracker;
using Data.Entities.Identity;
using Data.Entities.Child;
using Data.Entities.Community;

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
       // public DbSet<Student> students { get; set; }
       public DbSet<ChildProfile> ChildProfiles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<AbilityCategory> AbilityCategories { get; set; }
        public DbSet<AbilityQuestion> AbilityQuestions { get; set; }
        public DbSet<AbilityTestResult> AbilityTestResults { get; set; }
     
     
     
        // Community
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<CommunityComment> CommunityComments { get; set; }
        public DbSet<CommunityReaction> CommunityReactions { get; set; }
        public DbSet<CommunityReport> CommunityReports { get; set; }
     
     
        #region Views
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
   //         modelBuilder.ApplyConfiguration(new ChildConfiguration());
          modelBuilder.ApplyConfiguration(new CommunityPostConfiguration());
            modelBuilder.ApplyConfiguration(new CommunityCommentConfiguration());
            modelBuilder.ApplyConfiguration(new CommunityReactionConfiguration());
            modelBuilder.ApplyConfiguration(new CommunityReportConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
