using System.ComponentModel.DataAnnotations;

namespace Domain;

public class CanteenEmployee
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string EmployeeNumber { get; set; }
    
    [Required]
    public Canteen Canteen { get; set; }
}