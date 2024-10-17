using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Domain.Interface;

namespace Domain;

public class Student : IUserInfo
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public City City { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required, MaxLength(12)]
    public string StudentNumber { get; set; }
    
    public int Age => CalculateAge();
    
    private int CalculateAge()
    {
        var today = DateTime.Today;
        var age = today.Year - BirthDate.Year;
        if (BirthDate.Date > today.AddYears(-age)) age--; // Als verjaardag nog niet is geweest dit jaar, dan -1
        return age;
    }
}