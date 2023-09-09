 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnS.API.Data;
using SnS.API.Model.Domain;
using SnS.API.Model.DTO;

namespace SnS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SnSDbContext dbContext;

        public CategoryController(SnSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain Model
            var categoriesDomain = dbContext.Categories.ToList();
            //Map Deomain Model to DTOs
            var categoriesDto = new List<CategoryDto>();
            foreach (var categoryDomain in categoriesDomain)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = categoryDomain.Id,
                    Name = categoryDomain.Name,
                    Description = categoryDomain.Description
                });
            }


            return Ok(categoriesDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var categoryDomain = dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryDomain == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = categoryDomain.Id,
                Name = categoryDomain.Name,
                Description = categoryDomain.Description

            };
            //Return DTO back to client
            return Ok(categoryDto);
        }

        // POST to create new category
        [HttpPost]
        public IActionResult Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            //Map the Dto to domain Model
            var categoryDomain = new Category
            {
                Name = addCategoryRequestDto.Name,
                Description = addCategoryRequestDto.Description
            };

            //Use Domain model to create Category
            dbContext.Categories.Add(categoryDomain);
            dbContext.SaveChanges();

            //Map Domain Model back to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomain.Id,
                Name = categoryDomain.Name,
                Description = categoryDomain.Description

            };

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        //Update Category
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            var categoryDomain = dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (categoryDomain == null)
            {
                return NotFound();
            }

            //Map the Dto to domain Model
            categoryDomain.Name = updateCategoryRequestDto.Name;
            categoryDomain.Description = updateCategoryRequestDto.Description;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomain.Id,
                Name = categoryDomain.Name,
                Description = categoryDomain.Description
            };

            return Ok(categoryDto);

        }
    }
}
