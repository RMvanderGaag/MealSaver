﻿using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain;

public class MealPackage
{
    public Guid Id { get; set; }
    
    [Required]
    public string DescriptiveName { get; set; }
    
    [Required]
    public List<Product> Products { get; set; }
    
    [Required]
    public City City { get; set; }
    
    [Required]
    public Canteen Canteen { get; set; }
    
    [Required]
    public DateTime PickupTimeFrom { get; set; }
    
    [Required]
    public DateTime PickupTimeTo { get; set; }
    
    [Required]
    public bool Is18Plus { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public MealType MealType { get; set; }
    
    public Student ReservedBy { get; set; }
}