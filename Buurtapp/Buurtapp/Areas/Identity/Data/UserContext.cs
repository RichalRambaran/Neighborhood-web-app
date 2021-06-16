using Buurtapp.Areas.Identity.Data;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Buurtapp.Data
{
    public class UserContext : IdentityDbContext<AppUser>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatParticipant> ChatParticipants { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Solution> Solutions { get; set; }

        public DbSet<UserLikesPost> UserLikesPosts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportedPost> ReportedPosts { get; set; }

        public DbSet<ReportedComment> ReportedComments { get; set; }

        public DbSet<ReportedSolution> ReportedSolution { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Test data
            builder.Entity<Location>().HasData(new Location { PostalCode = "2526LX", HouseNumber = 917, Neighbourhood = "Schilderswijk" });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(p => p.Id).HasColumnName("UserId");
                entity.HasKey(p => p.Id);
                // Needed apparently
                //entity.Ignore(p => p.UserName);
                //entity.Ignore(p => p.NormalizedUserName);
                //entity.Ignore(p => p.NormalizedEmail);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.Property(p => p.Id).HasColumnName("RoleId");
                entity.HasKey(p => p.Id);
                // Needed apparently
                //entity.Ignore(p => p.NormalizedName);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRole");
                //in case you changed the TKey type
                //entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogin");
                //in case you changed the TKey type
                //entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaim");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserToken");
                //in case you changed the TKey type
                //entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            builder.Entity<AppUser>()
                .HasOne(p => p.Location)
                .WithMany(p => p.AppUsers)
                .HasForeignKey(p => new { p.PostalCode, p.HouseNumber });

            builder.Entity<Chat>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Chats)
                .HasForeignKey(p => p.UserId);

            builder.Entity<ChatParticipant>()
                .HasKey(p => new { p.ChatId, p.UserId });

            builder.Entity<ChatParticipant>()
                .HasOne(p => p.Chat)
                .WithMany(p => p.ChatParticipants)
                .HasForeignKey(p => p.ChatId);

            builder.Entity<ChatParticipant>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.ChatParticipants)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.PostId);

            builder.Entity<Comment>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Message>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Messages)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Location>()
                .HasKey(p => new { p.PostalCode, p.HouseNumber });

            builder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Solution>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Solutions)
                .HasForeignKey(p => p.PostId);

            builder.Entity<Solution>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Solutions)
                .HasForeignKey(p => p.UserId);

            builder.Entity<UserLikesPost>()
                .HasKey(p => new { p.UserId, p.PostId });

            builder.Entity<UserLikesPost>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.UserLikesPosts)
                .HasForeignKey(p => p.UserId);

            builder.Entity<UserLikesPost>()
                .HasOne(p => p.Post)
                .WithMany(p => p.UserLikesPosts)
                .HasForeignKey(p => p.PostId);

            builder.Entity<Report>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Reports)
                .HasForeignKey(p => p.UserId);

            builder.Entity<ReportedPost>()
                .HasOne(p => p.Post)
                .WithMany(p => p.ReportedPosts)
                .HasForeignKey(p => p.PostId);

            builder.Entity<ReportedComment>()
                .HasOne(p => p.Comment)
                .WithMany(p => p.ReportedComments)
                .HasForeignKey(p => p.CommentId);

            builder.Entity<ReportedSolution>()
                .HasOne(p => p.Solution)
                .WithMany(p => p.ReportedSolutions)
                .HasForeignKey(p => p.SolutionId);
        }
    }
}
