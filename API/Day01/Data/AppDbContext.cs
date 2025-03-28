using Day01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Day01.Data
{
    public class AppDbContext : IdentityDbContext<Student>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string studentRoleId = "d290f1ee-6c54-4b01-90e6-d701748f0851"; 
            string adminRoleId = "2lgkdsjhfkbda455";
            string adminUserId = "1ldkfaa58702m"; 

          
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = studentRoleId,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "a123b456-c789-d012-e345-f67890123456" 
                },
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "f123e456-d789-c012-b345-a67890123456"
                });

          
            string passwordHash = "AQAAAAEAACcQAAAAEDtuyx2lUOQqFJpQ==";

         
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = adminUserId,
                UserName = "admin",
                Address = "EG",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                Image = "adminphoto",
                PasswordHash = passwordHash, 
                SecurityStamp = "1234567890abcdef" 
            });

           
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });

            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string studentRoleId = "d290f1ee-6c54-4b01-90e6-d701748f0851";
        //    string adminRoleId = "2lgkdsjhfkbda455";

        //    modelBuilder.Entity<IdentityRole>().HasData(
        //        new IdentityRole()
        //        {
        //            Id = studentRoleId,
        //            Name = "Student",
        //            NormalizedName = "STUDENT",
        //            ConcurrencyStamp = "a123b456-c789-d012-e345-f67890123456",
        //        },
        //        new IdentityRole()
        //        {
        //            Id = adminRoleId,
        //            Name = "Admin",
        //            NormalizedName = "ADMIN",
        //            ConcurrencyStamp = "f123e456-d789-c012-b345-a67890123456",
        //        });

        //    string passwordHash = "AQAAAAEAACcQAAAAEDtuyx2lUOQqFJpQ==";

        //    modelBuilder.Entity<Student>().HasData(new Student
        //    {
        //        Id = "1ldkfaa58702m",
        //        UserName = "admin",
        //        Address = "EG",
        //        NormalizedUserName = "ADMIN",
        //        Email = "admin@example.com",
        //        NormalizedEmail = "ADMIN@EXAMPLE.COM",
        //        EmailConfirmed = true,
        //        Image = "adminphoto",
        //        PasswordHash = passwordHash,
        //        SecurityStamp = "1234567890abcdef",
        //    });

        //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //        new IdentityUserRole<string>
        //        {
        //            RoleId = adminRoleId,
        //            UserId = "1ldkfaa58702m"
        //        });

        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
