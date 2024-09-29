using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain;

public class Canteen
{
    public Guid Id { get; set; }
    
    [Required]
    public City City { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public bool OfferswarmMeals { get; set; }
    
    public List<MealPackage> MealPackages { get; set; }
}