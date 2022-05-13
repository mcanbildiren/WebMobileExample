using AdminTemplate.BusinessLogic.Repository.Abstracts;
using AdminTemplate.Models.Entities;
using AdminTemplate.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminTemplate.Components
{
    public class CategoryReportViewComponent : ViewComponent
    {
        private readonly IRepository<Category, int> _categoryRepo;

        public CategoryReportViewComponent(IRepository<Category, int> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            var data = _categoryRepo.Get()
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