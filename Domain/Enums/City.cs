using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum City
{
    Breda,
    [Display (Name = "Den Bosch")]
    DenBosch,
    Tilburg
}