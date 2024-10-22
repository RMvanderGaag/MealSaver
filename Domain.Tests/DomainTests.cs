using Domain.Enums;
using DomainServices.Repositories;
using DomainServices.Services;
using DomainServices.Services.Interface;
using Moq;

namespace Domain.Tests;

public class DomainTests
{
    [Fact]
    public Task UC2_AC2_CanteenEmployee_Sees_Other_Canteens_Offers()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();

        var canteen1 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var canteen2 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.Ld,
            City = City.Breda
        };
        
        var packages = new List<MealPackage>()
                    {
                        new MealPackage
                        {
                            Id = Guid.NewGuid(),
                            DescriptiveName = "Healthy Breakfast",
                            Products = new List<Product>(),
                            CanteenId = canteen1.Id,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
                            PickupTimeFrom = new DateTime(2024, 10, 1, 18, 0, 0),
                            PickupTimeTill = new DateTime(2024, 10, 1, 19, 30, 0),
                            Is18Plus = false,
                            Price = 12.99m,
                            MealType = MealType.Dinner,
                            ReservedById = Guid.NewGuid()
                        }
                    };
        
        mealPackageRepository.Setup(repo => repo.GetAllMealPackages()).Returns(packages.AsQueryable());

        var result = mealPackageRepository.Object.GetAllMealPackages().ToList();
        
        Assert.Equal(3, result.Count);
        return Task.CompletedTask;
    }

    [Fact]
    public async Task UC3_AC1_CanteenEmployee_Can_Use_Crud_On_Package()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var productRepository = new Mock<IProductRepository>();
        var canteenService = new CanteenService(mealPackageRepository.Object, productRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };
        
        productRepository.Setup(p => p.GetProductById(product.Id)).Returns(product);
        mealPackageRepository.Setup(m => m.CreateMealPackage(It.IsAny<MealPackage>())).ReturnsAsync(true);
        
        var addResult = canteenService.AddMealPackage(mealPackage.Id, mealPackage, new []{product.Id});
        Assert.True(await addResult);
        
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage.Id)).Returns(mealPackage);
        mealPackageRepository.Setup(m => m.DeleteMealPackage(It.IsAny<MealPackage>())).ReturnsAsync(true);
        
        var updateResult = canteenService.UpdateMealPackage(mealPackage.Id, mealPackage, new []{product.Id});
        Assert.True(await updateResult);
        
        var deleteResult = canteenService.DeleteMealPackage(mealPackage.Id);
        Assert.Equal("success", await deleteResult);
    }

    [Fact]
    public async Task UC3_AC2_CanteenEmployee_Can_Only_Edit_Or_Remove_A_Package_With_No_Reservations()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var productRepository = new Mock<IProductRepository>();
        var canteenService = new CanteenService(mealPackageRepository.Object, productRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = Guid.NewGuid()
        };
        
        productRepository.Setup(p => p.GetProductById(product.Id)).Returns(product);
        mealPackageRepository.Setup(m => m.CreateMealPackage(It.IsAny<MealPackage>())).ReturnsAsync(true);
        
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage.Id)).Returns(mealPackage);
        mealPackageRepository.Setup(m => m.DeleteMealPackage(It.IsAny<MealPackage>())).ReturnsAsync(true);
        
        var updateResult = canteenService.UpdateMealPackage(mealPackage.Id, mealPackage, new []{product.Id});
        Assert.False(await updateResult);
        
        var deleteResult = canteenService.DeleteMealPackage(mealPackage.Id);
        Assert.Equal("already-reserved", await deleteResult);
    }

    [Fact]
    public Task UC3_AC3_CanteenEmployee_Sees_Packages_At_Own_Location()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        
        var canteen1 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var canteen2 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.Ld,
            City = City.Breda
        };
        
        var canteenEmployee = new CanteenEmployee
        {
            Id = Guid.NewGuid(),
            Name = "Alice Johnson",
            EmployeeNumber = "EMP001",
            Email = "alice.johnson@mail.com",
            CanteenId = canteen1.Id
        };
        
        var packages = new List<MealPackage>()
                    {
                        new MealPackage
                        {
                            Id = Guid.NewGuid(),
                            DescriptiveName = "Healthy Breakfast",
                            Products = new List<Product>(),
                            CanteenId = canteen1.Id,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
                            PickupTimeFrom = new DateTime(2024, 10, 1, 18, 0, 0),
                            PickupTimeTill = new DateTime(2024, 10, 1, 19, 30, 0),
                            Is18Plus = false,
                            Price = 12.99m,
                            MealType = MealType.Dinner,
                            ReservedById = Guid.NewGuid()
                        }
                    };
        mealPackageRepository.Setup(repo => repo.GetAllMealPackages()).Returns(packages.AsQueryable());
        mealPackageRepository.Setup(repo => repo.GetAllCanteenPackages(canteenEmployee))
            .Returns(packages.Where(p => p.CanteenId == canteenEmployee.CanteenId).AsQueryable());
    
        var result = mealPackageRepository.Object.GetAllCanteenPackages(canteenEmployee).ToList();
    
        Assert.Single(result);
        Assert.Equal(canteen1.Id, result.First().CanteenId);

        return Task.CompletedTask;
    }

    [Fact]
    public Task UC4_AC1_Product_Has_Alcohol_Indicator()
    {
        var productRepository = new Mock<IProductRepository>();
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };
        
        productRepository.Setup(p => p.GetProductById(product.Id)).Returns(product);
        var result = productRepository.Object.GetProductById(product.Id);
        Assert.Equal("ContainsAlcohol", result.GetType().GetProperty("ContainsAlcohol")?.Name);
        return Task.CompletedTask;
    }

    [Fact]
    public async Task UC4_AC2_Package_Has_Eighteen_plus_Indicator_Automatically()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var productRepository = new Mock<IProductRepository>();
        var canteenService = new CanteenService(mealPackageRepository.Object, productRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = true,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };
        
        productRepository.Setup(p => p.GetProductById(product.Id)).Returns(product);
        mealPackageRepository.Setup(m => m.CreateMealPackage(It.IsAny<MealPackage>())).ReturnsAsync(true);
        
        var addResult = canteenService.AddMealPackage(mealPackage.Id, mealPackage, new []{product.Id});
        Assert.True(await addResult);
        Assert.True(mealPackage.Is18Plus);
    }

    [Fact]
    public async Task UC4_AC3_Student_Cant_Order_Eighteen_Plus_Package()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var productRepository = new Mock<IProductRepository>();
        var studentRepository = new Mock<IStudentRepository>();
        var mealPackageService = new MealPackageService(mealPackageRepository.Object, studentRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = true,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = true,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };
        
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            BirthDate = new DateTime(2008, 5, 10),
            Email = "john.doe@student.com",
            City = City.Breda,
            PhoneNumber = "0612345678",
            StudentNumber = "STU001"
        };
        
        productRepository.Setup(p => p.GetProductById(product.Id)).Returns(product);
        studentRepository.Setup(s => s.GetStudentById(student.Id)).Returns(student);
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage.Id)).Returns(mealPackage);
        mealPackageRepository.Setup(m => m.CreateMealPackage(mealPackage)).ReturnsAsync(true);
        
        var result = mealPackageService.ReserveMealPackage(mealPackage.Id, student.Id);
        Assert.Equal("not-eighteen", await result);
    }

    [Fact]
    public async Task UC5_AC1_Student_Can_Order_A_Package()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var productRepository = new Mock<IProductRepository>();
        var studentRepository = new Mock<IStudentRepository>();
        var mealPackageService = new MealPackageService(mealPackageRepository.Object, studentRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };
        
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            BirthDate = new DateTime(2008, 5, 10),
            Email = "john.doe@student.com",
            City = City.Breda,
            PhoneNumber = "0612345678",
            StudentNumber = "STU001"
        };
        
        studentRepository.Setup(s => s.GetStudentById(student.Id)).Returns(student);
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage.Id)).Returns(mealPackage);
        
        var result = mealPackageService.ReserveMealPackage(mealPackage.Id, student.Id);
        Assert.Equal("success", await result);
    }

    [Fact]
    public async Task UC5_AC2_Student_Can_Only_Order_One_Package_Per_Pickup_Day()
    {
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var studentRepository = new Mock<IStudentRepository>();
        var mealPackageService = new MealPackageService(mealPackageRepository.Object, studentRepository.Object);
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };

        var mealPackage2 = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Lunch Special",
            Products = new List<Product>(),
            CanteenId = Guid.NewGuid(),
            PickupTimeFrom = new DateTime(2024, 10, 1, 12, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 13, 30, 0),
            Is18Plus = false,
            Price = 7.49m,
            MealType = MealType.Lunch,
            ReservedById = null
        };
        
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            BirthDate = new DateTime(2008, 5, 10),
            Email = "john.doe@student.com",
            City = City.Breda,
            PhoneNumber = "0612345678",
            StudentNumber = "STU001"
        };
        
        studentRepository.Setup(s => s.GetStudentById(student.Id)).Returns(student);
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage.Id)).Returns(mealPackage);
        
        var result = mealPackageService.ReserveMealPackage(mealPackage.Id, student.Id);
        Assert.Equal("success", await result);
        
        var returnMealPackageList = new List<MealPackage> { mealPackage2 }; 
        mealPackageRepository.Setup(m => m.GetMealPackageById(mealPackage2.Id)).Returns(mealPackage2);
        mealPackageRepository.Setup(m => m.GetAllPackagesFromStudent(student.Id)).Returns(returnMealPackageList.AsQueryable());
        
        var result2 = mealPackageService.ReserveMealPackage(mealPackage2.Id, student.Id);
        Assert.Equal("already-reservation-on-this-day", await result2);
    }

    [Fact]
    public Task UC6_AC1_Package_Has_Products()
    {
        
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var studentRepository = new Mock<IStudentRepository>();
        
        var canteen = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Pear",
            ContainsAlcohol = false,
            Photo = "https://i.imgur.com/HLRqlU9.png"
        };
        
        var product2 = new Product()
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Hertog Jan",
            ContainsAlcohol = true,
            Photo = "https://i.imgur.com/InH4TUw.jpg"
        };

        var mealPackage = new MealPackage
        {
            Id = Guid.NewGuid(),
            DescriptiveName = "Healthy Breakfast",
            Products = new List<Product> { product, product2 },
            CanteenId = canteen.Id,
            PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
            PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
            Is18Plus = false,
            Price = 5.99m,
            MealType = MealType.Breakfast,
            ReservedById = null
        };
        
        Assert.Equal(2, mealPackage.Products.Count);
        return Task.CompletedTask;
    }

    [Fact]
    public Task UC8_AC1_2_Student_Can_Filter_Packages()
    {
        
        var mealPackageRepository = new Mock<IMealPackageRepository>();
        var studentRepository = new Mock<IStudentRepository>();
        var mealPackageService = new MealPackageService(mealPackageRepository.Object, studentRepository.Object);
        
        var canteen1 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.La,
            City = City.Breda
        };
        
        var canteen2 = new Canteen()
        {
            Id = Guid.NewGuid(),
            Location = Location.Ld,
            City = City.Breda
        };
        
        var packages = new List<MealPackage>()
                    {
                        new MealPackage
                        {
                            Id = Guid.NewGuid(),
                            DescriptiveName = "Healthy Breakfast",
                            Products = new List<Product>(),
                            CanteenId = canteen1.Id,
                            Canteen = canteen1,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
                            Canteen = canteen2,
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
                            Products = new List<Product>(),
                            CanteenId = canteen2.Id,
                            Canteen = canteen2,
                            PickupTimeFrom = new DateTime(2024, 10, 1, 18, 0, 0),
                            PickupTimeTill = new DateTime(2024, 10, 1, 19, 30, 0),
                            Is18Plus = false,
                            Price = 12.99m,
                            MealType = MealType.Dinner,
                            ReservedById = Guid.NewGuid()
                        }
                    };
        
        mealPackageRepository.Setup(repo => repo.GetAllUnReservedMealPackages()).Returns(packages.AsQueryable());
        
        var resultAllPackages = mealPackageService.FilterMealPackages(-1, -1).ToList();
        Assert.Equal(3, resultAllPackages.Count);
        
        var resultLocation = mealPackageService.FilterMealPackages((int)Location.La, -1).ToList();
        Assert.Single(resultLocation);
        
        var resultMealType = mealPackageService.FilterMealPackages(-1, (int)MealType.Lunch).ToList();
        Assert.Single(resultMealType);
        
        var resultBoth = mealPackageService.FilterMealPackages((int)Location.Ld, (int)MealType.Lunch).ToList();
        Assert.Single(resultBoth);
        
        return Task.CompletedTask;
    }
}