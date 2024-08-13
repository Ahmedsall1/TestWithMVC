using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWithMVC.Data;
using TestWithMVC.Models;


namespace TestWithMVC.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public ActionResult Index()
    {
        IEnumerable<Product> objProductList = _db.Products.ToList();
        return View(objProductList);
    }
    
    
    public ActionResult Create()
    {

        return View();
    }
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product obj)
    {

        if (ModelState.IsValid)
        {
            obj.ImagePath = Path.Combine("wwwroot/Images/Products", obj.ImageName);
            _db.Products.Add(obj);

            _db.SaveChanges();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public ActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var productFromDb = _db.Products.Find(id);

        if (productFromDb == null) return NotFound();

        return View(productFromDb);
    }
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, Product obj)
    {
        
        var newcat = _db.Products.Find(id);
        if (ModelState.IsValid)
        {
            newcat.Name = obj.Name;
            newcat.Category_name = obj.Category_name;
            newcat.Description = obj.Description;
            newcat.Price = obj.Price;
            newcat.ImageName = obj.ImageName;
            newcat.ImagePath = obj.ImagePath;
            newcat.CreatedDataTime = obj.CreatedDataTime;
            _db.SaveChanges();
            TempData["success"] = "Product updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public IActionResult Detail(int id)
    {
        var product = _db.Products.FirstOrDefault(p => p.ID == id);

        if (product == null)
            return NotFound();

        var category = _db.Categories.FirstOrDefault(c => c.Name == product.Category_name);

        if (category == null)
            return NotFound(); // Handle the case where the category is not found

        var viewModel = new ProductDetailViewModel
        {
            Category = category,
            Product = product
        };

        return View(viewModel);
    }
    public ActionResult Delete(int? id)
    {
        if(id == null || id==0) return NotFound();
        
        var productFromDb = _db.Products.Find(id);

        if(productFromDb == null) return NotFound();

        return View(productFromDb);
    }

    //post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj=_db.Products.Find(id);
        
        if(obj == null) return NotFound();
        
        _db.Products.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Product Deleted Successfully";
        
        return RedirectToAction("Index");
    }


}
