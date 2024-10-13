using Domain;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Account;

public class AccountDetailsModel(UserManager<IdentityUser> userManager, IStudentRepository studentRepository, SignInManager<IdentityUser> signInManager) : PageModel
{
    public Student Student { get; set; }
    
    public void OnGet()
    {
        Student = GetAccountDetails().Result;
    }
    
    private async Task<Student> GetAccountDetails()
    {
        var userId = userManager.GetUserId(HttpContext.User);
        var user = await userManager.FindByIdAsync(userId);
        var student = studentRepository.GetStudentByEmail(user.Email);
        return student;
    }
    
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await signInManager.SignOutAsync();
        return RedirectToPage("/Index");
    }
}