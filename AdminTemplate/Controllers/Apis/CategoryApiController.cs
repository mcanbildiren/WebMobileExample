using AdminTemplate.Data;
using AdminTemplate.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
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
    }
}
