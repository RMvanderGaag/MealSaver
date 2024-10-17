using System.Security.Claims;
using Domain;
using Domain.Interface;

namespace DomainServices.Services.Interface;

public interface IUserService
{
    Task<IUserInfo> GetLoggedInUserInfo(ClaimsPrincipal user);
}