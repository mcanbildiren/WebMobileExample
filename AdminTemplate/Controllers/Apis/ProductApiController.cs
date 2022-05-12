using AdminTemplate.Data;
using AdminTemplate.Dtos;
using AdminTemplate.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminTemplate.Controllers.Apis
{
    [ApiController]
    public class ProductApiController : BaseApiController
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public ProductApiController(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult All()
        {
            var products = _context.Products.Include(x => x.Category)
                .ToList()
                .Select(x => _mapper.Map<ProductDto>(x))
                .ToList();

            return Ok(products);
        }

        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            var product = _mapper.Map<ProductDto>(_context.Products.Find(id));
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Add(ProductDto model)
        {
            try
            {
                var data = _mapper.Map<Product>(model);
                data.CreatedUser = HttpContext.User.Identity!.Name;
                _context.Products.Add(data);
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Message = $"{data.Name} isimli ürün kaydedildi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult Update(Guid id, ProductDto model)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound(new { Success = false, Message = "Ürün bulunamadı" });
                }
                model.UpdatedUser = HttpContext.User.Identity!.Name;
                model.UpdatedDate = DateTime.UtcNow;
                product.Name = model.Name;
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Message = $"{model.Name} isimli ürün güncellendi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound(new { Success = false, Message = "Ürün bulunamadı" });
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Message = $"{product.Name} isimli ürün silindi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}