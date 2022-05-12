using AdminTemplate.Dtos.Abstracts;
using AdminTemplate.Models.Entities.Abstracts;

namespace AdminTemplate.Dtos
{
    public class CategoryDto : BaseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<ProductDto>? Products { get; set; }
    }
}