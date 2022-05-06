using AdminTemplate.Data;
using AdminTemplate.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminTemplate.Components
{
    public class CategoryReportViewComponent : ViewComponent
    {
        private readonly MyContext _context;

        public CategoryReportViewComponent(MyContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Categories
                .Include(x => x.Products)
                .Select(x => new CategoryReportViewModel()
                {
                    Name = x.Name,
                    ProductCount = x.Products.Count
                }).ToList();

            return View(data);
        }
    }
}