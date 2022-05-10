using AdminTemplate.Data;
using AdminTemplate.ViewModels;
using AdminTemplate.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers;

public class HomeController : Controller
{
    private readonly MyContext _context;
    // GET
    public HomeController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var productReportViewModel = new ProductReportViewModel()
        {
            Count = _context.Products.Count(),
            Total = _context.Products.Sum(x=>x.UnitPrice)
        };
        
        var model = new DashboardViewModels()
        {
            ProductReportViewModel = productReportViewModel
        };
        return View(model);
    }
    [HttpGet, Authorize]
    public IActionResult Category()
    {
        return View();
    }

    [HttpGet,Authorize]
    public IActionResult Product()
    {
        return View();
    }
}