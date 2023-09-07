using System;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace TestWithMVC.Models;
public class Category
{
    [Key]
    public int ID { get; set; }
    [Required]
    [StringLength( 10, ErrorMessage = "the Charactor count number it should be bettween 1 and 16")]
    public string Name { get; set; }
    public DateTime CreatedDataTime { get; set; }=DateTime.Now;

    public static implicit operator Category(List<Category> v)
    {
        throw new NotImplementedException();
    }
}


