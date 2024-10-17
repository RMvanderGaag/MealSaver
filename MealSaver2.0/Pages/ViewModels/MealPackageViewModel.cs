using Domain.Enums;

namespace MealSaver2._0.Pages.ViewModels;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class MealPackageViewModel
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Enter a name for the meal package")]
    [MaxLength(60), StringLength(60, ErrorMessage = "Name is too long")]
    public string DescriptiveName { get; set; }
    
    [Required(ErrorMessage = "Select at least one product")]
    public ICollection<Guid> SelectedProducts { get; set; }
    
    public Guid? CanteenId { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime PickupTimeTill { get; set; }
    
    [Required(ErrorMessage = "Enter a price for the meal package")]
    [Column(TypeName = "smallmoney")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Choose a meal type")]
    public MealType MealType { get; set; }
}