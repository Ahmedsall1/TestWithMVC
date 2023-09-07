using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TestWithMVC.Data;
using TestWithMVC.Models;

namespace TestWithMVC.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public ActionResult Index(){
        IEnumerable<Category> objCategoryList=_db.Categories.ToList();
        return View(objCategoryList);
    }
    //Get
    public ActionResult Create()
    {
        return View();
    }
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        foreach (var obj1 in _db.Categories.ToList()) {
            if (obj.Name == obj1.Name) {
                ModelState.AddModelError("name", "this Category is existed");
            }
        } 
        if(ModelState.IsValid){
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public ActionResult Edit(int? id)
    {
        if(id == null || id==0) return NotFound();
        
        var categoryFromDb = _db.Categories.Find(id);

        if(categoryFromDb == null) return NotFound();

        return View(categoryFromDb);
    }
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id,Category obj)
    {
        foreach (var obj1 in _db.Categories.ToList()) {
            if (obj.Name == obj1.Name) 
                ModelState.AddModelError("name", "this Category is existed");
        } 
        var newcat=_db.Categories.Find(id);
        if(ModelState.IsValid){
            newcat.Name = obj.Name;
            newcat.CreatedDataTime = obj.CreatedDataTime;
            _db.SaveChanges();
            TempData["success"] = "Category updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public ActionResult Delete(int? id)
    {
        if(id == null || id==0) return NotFound();
        
        var categoryFromDb = _db.Categories.Find(id);

        if(categoryFromDb == null) return NotFound();

        return View(categoryFromDb);
    }

    //post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj=_db.Categories.Find(id);
        
        if(obj == null) return NotFound();
        
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category Deleted Successfully";
        
        return RedirectToAction("Index");
    }

    public IActionResult Detail(string Category_name)
    {
        var category = _db.Categories.FirstOrDefault(c => c.Name == Category_name);
        if (category == null)
            return NotFound();
        
        var productsInCategory = _db.Products.Where(p => p.Category_name == Category_name).ToList();
        var viewModel = new CategoryDetailViewModel {
            Category = category,
            Products = productsInCategory
        };
        return View(viewModel);
    }

}