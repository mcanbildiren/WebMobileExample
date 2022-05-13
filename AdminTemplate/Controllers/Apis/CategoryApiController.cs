using AdminTemplate.BusinessLogic.Repository.Abstracts;
using AdminTemplate.Dtos;
using AdminTemplate.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers.Apis
{
    //[Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class CategoryApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category, int> _categoryRepo;

        public CategoryApiController(IMapper mapper, IRepository<Category, int> categoryRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }
        //CRUD -- Create Read Update Delete

        //[HttpGet] Read (SELECT)
        //[HttpPost] Create (INSERT)
        //[HttpPut] Update (UPDATE)
        //[HttpDelete] Delete (DELETE)

        [HttpGet]
        public IActionResult All()
        {
            try
            {
                //var data = _context.Categories
                //    //.Include(x => x.Products)
                //    .ToList()
                //    .Select(x => _mapper.Map<CategoryDto>(x))
                //    .ToList();

                var data = _categoryRepo.Get()
                    .ToList()
                    .Select(x => _mapper.Map<CategoryDto>(x));

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Add(CategoryDto model)
        {
            try
            {
                var data = _mapper.Map<Category>(model);
                //var data = new Category()
                //{
                //    Name = model.Name,
                //    Description = model.Description,
                //    CreatedUser = HttpContext.User.Identity!.Name
                //};
                data.CreatedUser = HttpContext.User.Identity!.Name;
                //_context.Categories.Add(data);
                //_context.SaveChanges();
                _categoryRepo.Insert(data);
                return Ok(new
                {
                    Success = true,
                    Message = $"{model.Name} isimli kategori başarıyla eklendi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult Detail(int id = 0)
        {
            try
            {
                //var data = _context.Categories.Find(id);
                var data = _categoryRepo.GetById(id);
                if (data == null)
                {
                    return NotFound(new { Message = $"{id} numaralı kategori bulunamadı" });
                }
                var model = _mapper.Map<CategoryDto>(data);
                //var model = new CategoryDto()
                //{
                //    Id = data.Id,
                //    Description = data.Description,
                //    Name = data.Name
                //};
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPut]
        public IActionResult Update(int id, CategoryDto model)
        {
            try
            {
                //var category = _context.Categories.Find(id);
                var category = _categoryRepo.GetById(id);

                if (category == null)
                {
                    return NotFound(new { Message = $"{id} numaralı kategori bulunamadı" });
                }
                category.Name = model.Name;
                category.Description = model.Description;
                //category = _mapper.Map<Category>(model);
                category.UpdatedUser = HttpContext.User.Identity!.Name;
                category.UpdatedDate = DateTime.UtcNow;
                //_context.SaveChanges();
                _categoryRepo.Update(category);
                return Ok(new
                {
                    Success = true,
                    Message = $"{category.Name} isimli kategori başarıyla güncellendi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                //var category = _context.Categories.Find(id);
                var category = _categoryRepo.GetById(id);

                if (category == null)
                {
                    return NotFound(new { Message = $"{id} numaralı kategori bulunamadı" });
                }
                //_context.Categories.Remove(category);
                //_context.SaveChanges();

                _categoryRepo.Delete(category);
                return Ok(new
                {
                    Success = true,
                    Message = $"{category.Name} isimli kategori başarıyla silindi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }
    }
}