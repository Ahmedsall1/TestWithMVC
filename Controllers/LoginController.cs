using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TestWithMVC.Models;

namespace TestWithMVC.Controllers;

public class LoginController : Controller
{
    public ActionResult Index(){
        return View();
    }
    public string Welcome(string name, int numTimes = 1) {
     return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
}
}