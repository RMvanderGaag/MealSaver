﻿using System.ComponentModel.DataAnnotations;
using Domain.Interface;

namespace Domain;

public class CanteenEmployee : IUserInfo
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string EmployeeNumber { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public Guid CanteenId { get; set; }
    public Canteen Canteen { get; set; }
}