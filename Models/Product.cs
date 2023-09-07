using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TestWithMVC.Controllers;
using Microsoft.EntityFrameworkCore;
using TestWithMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWithMVC.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  string  Description { get; set; }
    [Required]
    public string Category_name { get; set; }
    [Required]
    public decimal Price { get; set; }

    public string ImagePath { get; set; }

    public string ImageName { get; set; }

    public DateTime CreatedDataTime { get; set; } = DateTime.Now;
}


