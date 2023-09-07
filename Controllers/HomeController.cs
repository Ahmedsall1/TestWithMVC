using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestWithMVC.Models;
using Microsoft.EntityFrameworkCore;
using TestWithMVC.Data;
using System.Linq;

namespace TestWithMVC.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    public IActionResult Index2()
    {
    IEnumerable<Category> categories2 = _db.Categories.ToList();
    List<Product> product1= _db.Products.ToList();
    var viewModel = new CategoryDetailViewModel
    {
        Category = (Category)categories2,
        Products = product1

    };
    return View(viewModel);
}
    public IActionResult Index()
    {
         var categories = _db.Categories.ToList();
         var products= _db.Products.ToList();
         var viewModel = new HomeViewModel
        {
            Categories = categories,
            Products = products
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
