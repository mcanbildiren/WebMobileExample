using AdminTemplate.BusinessLogic.Repository.Abstracts.EntityFrameworkCore;
using AdminTemplate.Data;
using AdminTemplate.Models.Entities;

namespace AdminTemplate.BusinessLogic.Repository
{
    public class CategoryRepo : RepositoryBase<Category,int>
    {
        public CategoryRepo(MyContext context) : base(context)
        {
        }
    }
}