using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TopLearn.Datalayar.Entities.Course;
using TopLearn.Datalayar.Entities.Order;
using TopLearn.Datalayar.Entities.Permisions;
using TopLearn.Datalayar.Entities.User;
using TopLearn.Datalayar.Entities.Wallet;

namespace TopLearn.Datalayar.Context
{
   public class TopLearnContext:DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options):base(options)
        {
            
        }

        #region User

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<WalletType> WalletType { get; set; }

        #endregion

        #region Permision

        public DbSet<Permision> Permisions { get; set; }

        public DbSet<RoleToPermision> RoleToPermisions { get; set; }

        #endregion

        #region Course

        public DbSet<GroupCourse> GroupCourses { get; set; }
      
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
    
        public DbSet<Course> Courses { get; set; }
    
        public DbSet<CourseLevel> CourseLevels { get; set; }
   
        public DbSet<CourseStatus> CourseStatus { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        public DbSet<CourseComments> CourseComments { get; set; }

        public DbSet<CourseVote> CourseVotes { get; set; }
        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<GroupCourse>()
                .HasQueryFilter(g => !g.IsDelete);

            modelBuilder.Entity<Course>()
                .HasOne<GroupCourse>(g => g.CourseGroup)
                .WithMany(g => g.Courses)
                .HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<Course>()
                .HasOne<GroupCourse>(f => f.Group)
                .WithMany(g => g.SubGroup)
                .HasForeignKey(f => f.SubGroup);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(c => c.IsDelete == false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
