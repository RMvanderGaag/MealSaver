using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class MealSaverEFDBContext(DbContextOptions<MealSaverEFDBContext> contextOptions) : DbContext(contextOptions)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<MealPackage> MealPackages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Canteen> Canteens { get; set; }
    public DbSet<CanteenEmployee> CanteenEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Student
        IEnumerable<Student> students = new List<Student>
        {
            new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BirthDate = new DateTime(1995, 5, 10),
                Email = "john.doe@student.com",
                City = City.Breda,
                PhoneNumber = "0612345678",
                StudentNumber = "STU001"
            },
            new Student
            {
                Id = Guid.NewGuid(),
                Name = "Jane Smith",
                BirthDate = new DateTime(1997, 8, 25),
                Email = "jane.smith@example.com",
                City = City.DenBosch,
                PhoneNumber = "0612345678",
                StudentNumber = "STU002"
            },
            new Student
            {
                Id = Guid.NewGuid(),
                Name = "Michael Johnson",
                BirthDate = new DateTime(1996, 11, 3),
                Email = "michael.johnson@example.com",
                City = City.Tilburg,
                PhoneNumber = "0612345678",
                StudentNumber = "STU003"
            },
            new Student
            {
                Id = Guid.NewGuid(),
                Name = "Emily Davis",
                BirthDate = new DateTime(2005, 2, 18),
                Email = "emily.davis@example.com",
                City = City.Breda,
                PhoneNumber = "0612345678",
                StudentNumber = "STU004"
            },
            new Student
            {
                Id = Guid.NewGuid(),
                Name = "William Brown",
                BirthDate = new DateTime(1999, 12, 9),
                Email = "william.brown@example.com",
                City = City.DenBosch,
                PhoneNumber = "0612345678",
                StudentNumber = "STU005"
            }
        };
        
        //Canteen
        IEnumerable<Canteen> canteens = new List<Canteen>
        {
            new Canteen
            {
                Id = Guid.NewGuid(),
                City = City.Breda,
                Location = Location.La,
                OffersWarmMeals = true
            },
            new Canteen
            {
                Id = Guid.NewGuid(),
                City = City.Breda,
                Location = Location.Ld,
                OffersWarmMeals = false
            },
            new Canteen
            {
                Id = Guid.NewGuid(),
                City = City.Breda,
                Location = Location.Ha,
                OffersWarmMeals = true
            }
        };
        
        var product1 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Appel",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/Dy86B5w.png"
        };
        var product2 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Peer",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };
        var product3 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Broodje Frikandel",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/G6puzUN.png"

        };
        var product4 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Kaiserbroodje",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/rMdiQiP.png"

        };
        var product5 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Hertog Jan",
            ContainsAlcohol = true,
            Photo = "https://i.imgur.com/InH4TUw.jpg"
        };
        
        IEnumerable<MealPackage> mealPackages = new List<MealPackage>
        {
            new MealPackage
            {
                Id = Guid.NewGuid(),
                DescriptiveName = "Healthy Breakfast",
                Products = [],
                CanteenId = canteens.ToList()[0].Id,
                PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
                PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
                Is18Plus = false,
                Price = 5.99m,
                MealType = MealType.Breakfast,
                ReservedById = null
            },
            new MealPackage
            {
                Id = Guid.NewGuid(),
                DescriptiveName = "Lunch Special",
                Products = [],
                CanteenId = canteens.ToList()[1].Id,
                PickupTimeFrom = new DateTime(2024, 10, 1, 12, 0, 0),
                PickupTimeTill = new DateTime(2024, 10, 1, 13, 30, 0),
                Is18Plus = false,
                Price = 7.49m,
                MealType = MealType.Lunch,
                ReservedById = null
            },
            new MealPackage
            {
                Id = Guid.NewGuid(),
                DescriptiveName = "Dinner Combo",
                Products = [],
                CanteenId = canteens.ToList()[2].Id,
                PickupTimeFrom = new DateTime(2024, 10, 1, 18, 0, 0),
                PickupTimeTill = new DateTime(2024, 10, 1, 19, 30, 0),
                Is18Plus = false,
                Price = 12.99m,
                MealType = MealType.Dinner,
                ReservedById = null
            },
            new MealPackage
            {
                Id = Guid.NewGuid(),
                DescriptiveName = "Snack Pack",
                Products = [],
                CanteenId = canteens.ToList()[0].Id,
                PickupTimeFrom = new DateTime(2024, 10, 1, 15, 0, 0),
                PickupTimeTill = new DateTime(2024, 10, 1, 16, 30, 0),
                Is18Plus = false,
                Price = 3.49m,
                MealType = MealType.Snack,
                ReservedById = null
            },
            new MealPackage
            {
                Id = Guid.NewGuid(),
                DescriptiveName = "Evening Beers",
                Products = [],
                CanteenId = canteens.ToList()[2].Id,
                PickupTimeFrom = new DateTime(2024, 10, 1, 20, 0, 0),
                PickupTimeTill = new DateTime(2024, 10, 1, 22, 30, 0),
                Is18Plus = true,
                Price = 8.99m,
                MealType = MealType.AlcoholicBeverage,
                ReservedById = null
            }
        };
        
        IEnumerable<CanteenEmployee> canteenEmployees = new List<CanteenEmployee>
        {
            new CanteenEmployee
            {
                Id = Guid.NewGuid(),
                Name = "Alice Johnson",
                EmployeeNumber = "EMP001",
                Email = "alice.johnson@mail.com",
                CanteenId = canteens.ToList()[0].Id
            },
            new CanteenEmployee
            {
                Id = Guid.NewGuid(),
                Name = "Bob Smith",
                EmployeeNumber = "EMP002",
                Email = "bob.smith@mail.com",
                CanteenId = canteens.ToList()[1].Id,
            },
        };
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>().HasData(students);
        modelBuilder.Entity<Canteen>().HasData(canteens);
        modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5);
        modelBuilder.Entity<MealPackage>().HasData(mealPackages);
        modelBuilder.Entity<CanteenEmployee>().HasData(canteenEmployees);
        
        modelBuilder.Entity<MealPackage>().HasOne(c => c.Canteen).WithMany(c => c.MealPackages);
        modelBuilder.Entity<MealPackage>().HasMany(mp => mp.Products)
            .WithMany(p => p.MealPackages)
            .UsingEntity<Dictionary<string, object>>(
                "MealPackage_Product",
                r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                l => l.HasOne<MealPackage>().WithMany().HasForeignKey("MealPackageId"),
                je =>
                {
                    je.HasKey("MealPackageId", "ProductId");
                    je.HasData(
                        new { MealPackageId = mealPackages.ToList()[0].Id, ProductId = product1.Id },
                        new { MealPackageId = mealPackages.ToList()[0].Id, ProductId = product2.Id },
                        new { MealPackageId = mealPackages.ToList()[1].Id, ProductId = product3.Id },
                        new { MealPackageId = mealPackages.ToList()[1].Id, ProductId = product4.Id },
                        new { MealPackageId = mealPackages.ToList()[2].Id, ProductId = product4.Id },
                        new { MealPackageId = mealPackages.ToList()[2].Id, ProductId = product5.Id }
                    );
                });
        
        modelBuilder.Entity<CanteenEmployee>().HasOne(c => c.Canteen);

    }
}