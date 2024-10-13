using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class MealSaverIFDBContext(DbContextOptions<MealSaverIFDBContext> contextOptions) : IdentityDbContext(contextOptions)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Roles
        var canteenEmployeeRole = new IdentityRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "CanteenEmployee",
            NormalizedName = "CanteenEmployee".ToUpper()
        };
        
        var studentRole = new IdentityRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Student",
            NormalizedName = "Student".ToUpper()
        };
        
        builder.Entity<IdentityRole>().HasData(canteenEmployeeRole, studentRole);
        
        //Users
        var hasher = new PasswordHasher<IdentityUser>();

        var canteenEmployee1 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            NormalizedUserName = "Alice".ToUpper(),
            Email = "alice.johnson@mail.com",
            NormalizedEmail = "alice.johnson@mail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "12345")
        };

        var canteenEmployee2 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Bob",
            NormalizedUserName = "Bob".ToUpper(),
            Email = "bob.smith@mail.com",
            NormalizedEmail = "bob.smith@mail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };
        
        var student1 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "John Doe",
            NormalizedUserName = "John Doe".ToUpper(),
            Email = "john.doe@student.com",
            NormalizedEmail = "john.doe@student.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };

        var student2 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Jane Smith",
            NormalizedUserName = "Jane Smith".ToUpper(),
            Email = "jane.smith@example.com",
            NormalizedEmail = "jane.smith@example.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };

        var student3 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Michael Johnson",
            NormalizedUserName = "Michael Johnson".ToUpper(),
            Email = "michael.johnson@example.com",
            NormalizedEmail = "michael.johnson@example.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };

        var student4 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Emily Davis",
            NormalizedUserName = "Emily Davis".ToUpper(),
            Email = "emily.davis@example.com",
            NormalizedEmail = "emily.davis@example.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };

        var student5 = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "William Brown",
            NormalizedUserName = "William Brown".ToUpper(),
            Email = "william.brown@example.com",
            NormalizedEmail = "william.brown@example.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "123456")
        };
        
        builder.Entity<IdentityUser>().HasData(canteenEmployee1, canteenEmployee2, student1, student2, student3, student4, student5);
        
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = canteenEmployeeRole.Id,
                UserId = canteenEmployee1.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = canteenEmployeeRole.Id,
                UserId = canteenEmployee2.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = studentRole.Id,
                UserId = student1.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = studentRole.Id,
                UserId = student2.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = studentRole.Id,
                UserId = student3.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = studentRole.Id,
                UserId = student4.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = studentRole.Id,
                UserId = student5.Id
            }
        );
    }
}