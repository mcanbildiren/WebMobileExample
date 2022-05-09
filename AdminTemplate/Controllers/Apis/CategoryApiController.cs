using AdminTemplate.Data;
using AdminTemplate.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers.Apis
{
    //[Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class CategoryApiController : BaseApiController
    {
        private readonly MyContext _context;

        public CategoryApiController(MyContext context)
        {
            _context = context;
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
                return Ok(_context.Categories.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Add(Category model)
        {
            try
            {
                model.CreatedUser = HttpContext.User.Identity!.Name;
                _context.Categories.Add(model);
                _context.SaveChanges();
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
                return Ok(_context.Categories.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPut]
        public IActionResult Update(int id, Category model)
        {
            try
            {
                var category = _context.Categories.Find(id);

                if (category == null)
                {
                    return NotFound(new { Message = $"{id} numaralı kategori bulunamadı" });
                }

                category.Name = model.Name;
                category.Description = model.Description;
                category.UpdatedUser = HttpContext.User.Identity!.Name;
                category.UpdatedDate = DateTime.UtcNow;
                _context.SaveChanges();
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
                var category = _context.Categories.Find(id);

                if (category == null)
                {
                    return NotFound(new { Message = $"{id} numaralı kategori bulunamadı" });
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
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