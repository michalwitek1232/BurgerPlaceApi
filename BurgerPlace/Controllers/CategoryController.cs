﻿using AutoMapper;
using BurgerPlace.Context;
using BurgerPlace.Models.Database;
using BurgerPlace.Models.Request.Ingredients;
using BurgerPlace.Models.Response.Categories;
using BurgerPlace.Models.Response.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BurgerPlace.Models.Response.Other.CommonResponse;

namespace BurgerPlace.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.UserRoles.User)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMapper _mapper;
        public CategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all <see cref="Category">Categories</see> and maps it to list
        /// </summary>
        /// <returns>
        /// List of <see cref="CategoryMapped">Categories</see> 
        /// </returns>
        [HttpGet()]
        public async Task<IActionResult> GetCategories()
        {
            using (var context = new BurgerPlaceContext())
            {
                var categories = await context.Categories.ToListAsync();
                var mapped = _mapper.Map<List<CategoryMapped>>(categories);
                return Ok(mapped);
            }
        }

        /// <summary>
        /// Creates new <see cref="Category"></see>
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(Duplicated), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Created), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory createCategory)
        {
            using (var context = new BurgerPlaceContext())
            {
                if (await context.Categories.Where(x => x.Name == createCategory.Name).FirstOrDefaultAsync() != null)
                {
                    return BadRequest(new Duplicated());
                }
                Category category = new Category();
                category.Name = createCategory.Name;
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Ok(new CommonResponse.Created());
            }
        }

        /// <summary>
        /// Updates <see cref="Category"></see> by it's Id
        /// </summary>
        /// <param name="createCategory">Model of new <see cref="Category"/></param>
        /// <param name="id">Id of <see cref="Category"/> we want to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Updated), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategory createCategory, int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var category = await context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null)
                {
                    return NotFound(new NotFoundWithThisId());
                }

                category.Name = createCategory.Name;
                await context.SaveChangesAsync();
                return Ok(new CommonResponse.Updated());
            }
        }
        /// <summary>
        /// Removing <see cref="Category"/> by it's Id
        /// </summary>
        /// <param name="id">Id of <see cref="Category"/> we want to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NotFoundWithThisId), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RemovedName), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            using (var context = new BurgerPlaceContext())
            {
                var category = await context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null)
                {
                    return NotFound(new NotFoundWithThisId());
                }

                context.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new CommonResponse.RemovedName(category.Name));
            }
        }

    }
}
