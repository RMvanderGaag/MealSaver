using Domain;
using Domain.Interface;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Account;

public class AccountDetailsModel(IUserService userService, SignInManager<IdentityUser> signInManager) : PageModel
{
    public IUserInfo user { get; set; }
    public Student Student { get; set; }
    public CanteenEmployee CanteenEmployee { get; set; }
    
    public void OnGet()
    {
        user = userService.GetLoggedInUserInfo(User).Result;

        if (User.IsInRole("Student")) Student = user as Student ?? null;
        else if(User.IsInRole("CanteenEmployee")) CanteenEmployee = user as CanteenEmployee ?? null;
    }
    
    
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await signInManager.SignOutAsync();
        return RedirectToPage("/Index");
    }
}