using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    public Guid Id { get; set; }
    
    [Required]
    public string DescriptiveName { get; set; }
    
    [Required]
    public bool ContainsAlcohol { get; set; }
    
    [Required]
    public string Photo { get; set; }
    
    public ICollection<MealPackage> MealPackages { get; set; }
}