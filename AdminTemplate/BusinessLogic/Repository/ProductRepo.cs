using System.Linq.Expressions;
using AdminTemplate.BusinessLogic.Repository.Abstracts.EntityFrameworkCore;
using AdminTemplate.Data;
using AdminTemplate.Models.Entities;
using AdminTemplate.ViewModels.Dashboard;

namespace AdminTemplate.BusinessLogic.Repository
{
    public class ProductRepo : RepositoryBase<Product, Guid>
    {
        public ProductRepo(MyContext context) : base(context)
        {
        }

        public override IQueryable<Product> Get(Expression<Func<Product, bool>> predicate = null)
        {
            return predicate == null ? _table.OrderBy(x => x.Name) : _table.Where(predicate).OrderBy(x => x.Name);
        }

        public ProductReportViewModel GetProductReport()
        {
            var productReportViewModel = new ProductReportViewModel()
            {
                Count = this.Get().Count(),
                Total = this.Get().Sum(x => x.UnitPrice)
            };

            return productReportViewModel;
        }
    }
}