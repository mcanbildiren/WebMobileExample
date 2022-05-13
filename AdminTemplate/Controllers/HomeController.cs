using AdminTemplate.BusinessLogic.Repository;
using AdminTemplate.BusinessLogic.Repository.Abstracts;
using AdminTemplate.Models.Entities;
using AdminTemplate.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers;

public class HomeController : Controller
{
    //private readonly MyContext _context;
    private readonly IRepository<Product, Guid> _productRepo;

    // GET
    public HomeController(IRepository<Product, Guid> productRepo)
    {
        _productRepo = productRepo;
    }

    public IActionResult Index()
    {
        var productReportViewModel = (_productRepo as ProductRepo).GetProductReport();
        
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