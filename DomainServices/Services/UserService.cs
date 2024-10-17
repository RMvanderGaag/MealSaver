using System.Security.Claims;
using Domain.Interface;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace DomainServices.Services;

public class UserService(UserManager<IdentityUser> userManager, IStudentRepository studentRepository, ICanteenEmployeeRepository canteenEmployeeRepository) : IUserService
{
    public async Task<IUserInfo> GetLoggedInUserInfo(ClaimsPrincipal user)
    {
        var userId = userManager.GetUserId(user);
        var u = await userManager.FindByIdAsync(userId);

        if (await userManager.IsInRoleAsync(u, "Student"))
        {
            var student = studentRepository.GetStudentByEmail(u.Email);
            return student;
        }
        else if (await userManager.IsInRoleAsync(u, "CanteenEmployee"))
        {
            var canteenEmployee = canteenEmployeeRepository.GetCanteenEmployeeByEmail(u.Email);
            return canteenEmployee;
        }

        return null;
    }
}