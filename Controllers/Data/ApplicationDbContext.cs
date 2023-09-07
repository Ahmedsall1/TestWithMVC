using Microsoft.EntityFrameworkCore;
using TestWithMVC.Models;

namespace TestWithMVC.Data;

public class ApplicationDbContext : DbContext{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

    }
    
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

}