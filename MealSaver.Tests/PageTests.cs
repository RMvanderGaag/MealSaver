using System.Security.Claims;
using Domain;
using Domain.Enums;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using MealSaver2._0.Pages.Account;
using MealSaver2._0.Pages.Canteen;
using MealSaver2._0.Pages.MealPackage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace MealSaver.Tests
{
    public class PageTests
    {
        [Fact]
        public void UC1_AC1_Package_Index_Should_Return_Unreserved_Packages()
        {
            var mealPackageService = new Mock<IMealPackageService>();
            var pageModel = new MealPackagesModel(mealPackageService.Object);

            var packages = new List<MealPackage>()
            {
                new MealPackage
                {
                    Id = Guid.NewGuid(),
                    DescriptiveName = "Healthy Breakfast",
                    Products = new List<Product>(),
                    CanteenId = Guid.NewGuid(),
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
                    CanteenId = Guid.NewGuid(),
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
                    CanteenId = Guid.NewGuid(),
                    PickupTimeFrom = new DateTime(2024, 10, 1, 18, 0, 0),
                    PickupTimeTill = new DateTime(2024, 10, 1, 19, 30, 0),
                    Is18Plus = false,
                    Price = 12.99m,
                    MealType = MealType.Dinner,
                    ReservedById = Guid.NewGuid()
                }
            };

            mealPackageService.Setup(x => x.FilterMealPackages(-1, -1))
                .Returns(packages.AsQueryable());

            pageModel.OnGet();

            Assert.Equal(2, pageModel.AllMealPackages.Count(x => x.ReservedById == null));
            Assert.All(pageModel.AllMealPackages.Where(x => x.ReservedById == null), package =>
            {
                Assert.Null(package.ReservedById);
            });
        }

        [Fact]
        public async Task UC1_AC2_MyPackages_Should_Return_Student_Reserved_Packages()
        {
            var user = new Mock<IUserStore<IdentityUser>>();
            var identityUser = new IdentityUser()
            {
                Id = "2fc4c69d-3efd-41b8-8b57-7b45e0457b2d",
                UserName = "John Doe",
                NormalizedUserName = "John Doe".ToUpper(),
                Email = "john.doe@student.com",
                NormalizedEmail = "john.doe@student.com".ToUpper(),
            };

            user.Setup(x => x.FindByIdAsync("2fc4c69d-3efd-41b8-8b57-7b45e0457b2d", CancellationToken.None))
                .ReturnsAsync(identityUser);

            var mealPackageRepository = new Mock<IMealPackageRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(user.Object, null, null, null, null, null, null, null, null);
            var userService = new Mock<IUserService>();
            var pageModel = new ReservedMealPackagesModel(mealPackageRepository.Object, userService.Object);
            
            var userPrinicipal = new Mock<ClaimsPrincipal>();
            pageModel.PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrinicipal.Object
                }
            };
            
            userManagerMock.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(identityUser);
            userManagerMock.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<IdentityUser>(), "Student"));

            var id = Guid.NewGuid();
            var student = new Student
            {
                Id = id,
                Name = "John Doe",
                BirthDate = new DateTime(1995, 5, 10),
                Email = "john.doe@student.com",
                City = City.Breda,
                PhoneNumber = "0612345678",
                StudentNumber = "STU001"
            };
            
            userService.Setup(x => x.GetLoggedInUserInfo(pageModel.HttpContext.User)).ReturnsAsync(student);
            
            var packages = new List<MealPackage>()
            {
                new MealPackage
                {
                    Id = Guid.NewGuid(),
                    DescriptiveName = "Healthy Breakfast",
                    Products = new List<Product>(),
                    CanteenId = Guid.NewGuid(),
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
                    CanteenId = Guid.NewGuid(),
                    PickupTimeFrom = new DateTime(2024, 10, 1, 12, 0, 0),
                    PickupTimeTill = new DateTime(2024, 10, 1, 13, 30, 0),
                    Is18Plus = false,
                    Price = 7.49m,
                    MealType = MealType.Lunch,
                    ReservedById = id
                },
            };
            
            mealPackageRepository.Setup(m => m.GetAllPackagesFromStudent(student.Id)).Returns(packages.Where(p => p.ReservedById == student.Id).AsQueryable());
            
            await pageModel.OnGetAsync();
            
            Assert.Equal(1, pageModel.ReservedMealPackages.Count());
            Assert.All(pageModel.ReservedMealPackages, package =>
            {
                Assert.Equal(student.Id, package.ReservedById);
            });
        }

        [Fact]
        public async Task UC2_AC1_CanteenEmployee_Sees_Own_Canteen_Offers()
        {
            var user = new Mock<IUserStore<IdentityUser>>();
            var identityUser = new IdentityUser()
            {
                Id = "2fc4c69d-3efd-41b8-8b57-7b45e0457b2d",
                UserName = "Alice",
                NormalizedUserName = "Alice".ToUpper(),
                Email = "alice.johnson@mail.com",
                NormalizedEmail = "alice.johnson@mail.com".ToUpper(),
            };

            user.Setup(x => x.FindByIdAsync("2fc4c69d-3efd-41b8-8b57-7b45e0457b2d", CancellationToken.None))
                .ReturnsAsync(identityUser);
            
            var canteenService = new Mock<ICanteenService>();
            var mealPackageRepository = new Mock<IMealPackageRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(user.Object, null, null, null, null, null, null, null, null);
            var userService = new Mock<IUserService>();
            var mockTempData = new Mock<ITempDataDictionary>();
            
            var pageModel = new CanteenMealPackagesModel(mealPackageRepository.Object, canteenService.Object, userService.Object);
            pageModel.TempData = mockTempData.Object;
            
            var userPrinicipal = new Mock<ClaimsPrincipal>();
            pageModel.PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrinicipal.Object
                }
            };
            
            userManagerMock.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(identityUser);
            userManagerMock.Setup(userManager =>
                userManager.IsInRoleAsync(It.IsAny<IdentityUser>(), "CanteenEmployee"));

            var canteen1 = new Canteen
            {
                Id = Guid.NewGuid(),
                City = City.Breda,
                Location = Location.La,
                OffersWarmMeals = true
            };
            var canteen2 = new Canteen
            {
                Id = Guid.NewGuid(),
                City = City.Breda,
                Location = Location.Ld,
                OffersWarmMeals = false
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
            
            userService.Setup(x => x.GetLoggedInUserInfo(pageModel.HttpContext.User)).ReturnsAsync(canteenEmployee);
            
            mealPackageRepository.Setup(m => m.GetAllCanteenPackages(canteenEmployee)).Returns(packages.Where(p => p.CanteenId == canteenEmployee.CanteenId).AsQueryable());
            
            await pageModel.OnGet();
            
            Assert.Equal(1, pageModel.MealPackages.Count());
            Assert.All(pageModel.MealPackages, package =>
            {
                Assert.Equal(canteenEmployee.CanteenId, package.CanteenId);
            });
        }

        [Fact]
        public async Task UC7_AC1_Package_Already_Reserved_Gives_Error()
        {
            var user = new Mock<IUserStore<IdentityUser>>();
            var identityUser = new IdentityUser()
            {
                Id = "2fc4c69d-3efd-41b8-8b57-7b45e0457b2d",
                UserName = "John Doe",
                NormalizedUserName = "John Doe".ToUpper(),
                Email = "john.doe@student.com",
                NormalizedEmail = "john.doe@student.com".ToUpper(),
            };

            user.Setup(x => x.FindByIdAsync("2fc4c69d-3efd-41b8-8b57-7b45e0457b2d", CancellationToken.None))
                .ReturnsAsync(identityUser);

            var mealPackageRepository = new Mock<IMealPackageRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(user.Object, null, null, null, null, null, null, null, null);
            var userService = new Mock<IUserService>();
            var mealPackageService = new Mock<IMealPackageService>();
            var pageModel = new MealPackageDetailsModel(mealPackageRepository.Object, mealPackageService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            pageModel.TempData = mockTempData.Object;
            
            var userPrincipal = new Mock<ClaimsPrincipal>();
            pageModel.PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal.Object
                }
            };
            
            userManagerMock.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(identityUser);
            userManagerMock.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<IdentityUser>(), "Student"));

            var id = Guid.NewGuid();
            var student = new Student
            {
                Id = id,
                Name = "John Doe",
                BirthDate = new DateTime(1995, 5, 10),
                Email = "john.doe@student.com",
                City = City.Breda,
                PhoneNumber = "0612345678",
                StudentNumber = "STU001"
            };
            
            userService.Setup(x => x.GetLoggedInUserInfo(pageModel.HttpContext.User)).ReturnsAsync(student);
            
            var packages = new List<MealPackage>()
            {
                new MealPackage
                {
                    Id = Guid.NewGuid(),
                    DescriptiveName = "Healthy Breakfast",
                    Products = new List<Product>(),
                    CanteenId = Guid.NewGuid(),
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
                    CanteenId = Guid.NewGuid(),
                    PickupTimeFrom = new DateTime(2024, 10, 1, 12, 0, 0),
                    PickupTimeTill = new DateTime(2024, 10, 1, 13, 30, 0),
                    Is18Plus = false,
                    Price = 7.49m,
                    MealType = MealType.Lunch,
                    ReservedById = id 
                },
            };

            mealPackageRepository.Setup(m => m.GetMealPackageById(packages[1].Id)).Returns(packages[1]);
            mealPackageService.Setup(m => m.ReserveMealPackage(packages[1].Id, student.Id)).ReturnsAsync("already-reserved");

            pageModel.MealPackage = packages[1];
            await pageModel.OnPostAsync(packages[1].Id);

            Assert.Equal("Meal package already reserved", pageModel.Message);
        }

        [Fact]
        public async Task UC7_AC2_Reserve_Package_Succesfully()
        {
            var user = new Mock<IUserStore<IdentityUser>>();
            var identityUser = new IdentityUser()
            {
                Id = "2fc4c69d-3efd-41b8-8b57-7b45e0457b2d",
                UserName = "John Doe",
                NormalizedUserName = "John Doe".ToUpper(),
                Email = "john.doe@student.com",
                NormalizedEmail = "john.doe@student.com".ToUpper(),
            };

            user.Setup(x => x.FindByIdAsync("2fc4c69d-3efd-41b8-8b57-7b45e0457b2d", CancellationToken.None))
                .ReturnsAsync(identityUser);

            var mealPackageRepository = new Mock<IMealPackageRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(user.Object, null, null, null, null, null, null, null, null);
            var userService = new Mock<IUserService>();
            var mealPackageService = new Mock<IMealPackageService>();
            var pageModel = new MealPackageDetailsModel(mealPackageRepository.Object, mealPackageService.Object, userService.Object);
            var mockTempData = new Mock<ITempDataDictionary>();
            pageModel.TempData = mockTempData.Object;
            
            var userPrincipal = new Mock<ClaimsPrincipal>();
            pageModel.PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal.Object
                }
            };
            
            userManagerMock.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(identityUser);
            userManagerMock.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<IdentityUser>(), "Student"));

            var id = Guid.NewGuid();
            var student = new Student
            {
                Id = id,
                Name = "John Doe",
                BirthDate = new DateTime(1995, 5, 10),
                Email = "john.doe@student.com",
                City = City.Breda,
                PhoneNumber = "0612345678",
                StudentNumber = "STU001"
            };
            
            userService.Setup(x => x.GetLoggedInUserInfo(pageModel.HttpContext.User)).ReturnsAsync(student);
            
            var packages = new List<MealPackage>()
            {
                new MealPackage
                {
                    Id = Guid.NewGuid(),
                    DescriptiveName = "Healthy Breakfast",
                    Products = new List<Product>(),
                    CanteenId = Guid.NewGuid(),
                    PickupTimeFrom = new DateTime(2024, 10, 1, 8, 0, 0),
                    PickupTimeTill = new DateTime(2024, 10, 1, 9, 30, 0),
                    Is18Plus = false,
                    Price = 5.99m,
                    MealType = MealType.Breakfast,
                    ReservedById = null
                }
            };
            
            mealPackageRepository.Setup(m => m.GetMealPackageById(packages[0].Id)).Returns(packages[0]);
            
            mealPackageService.Setup(m => m.ReserveMealPackage(packages[0].Id, student.Id)).ReturnsAsync("success");

            pageModel.MealPackage = packages[0];
            await pageModel.OnPostAsync(packages[0].Id);

            Assert.Equal("Meal package successfully reserved", pageModel.Message);
        }
    }
}
