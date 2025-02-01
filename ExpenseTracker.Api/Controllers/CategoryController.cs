using AutoMapper;
using ExpenseTracker.Application.DTOs.Category;
using ExpenseTracker.Application.Mediator.Categories.Commands;
using ExpenseTracker.Application.Mediator.Categories.Queries;
using ExpenseTracker.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!_userManager.Users.Any(u => u.Id == userId))
            {
                return NotFound("User not found!");
            }

            var command = new CreateCategoryCommand(
                UserId: userId,
                Name: dto.Name
            );

            var categoryId = await _mediator.Send(command);

            return Ok(new { categoryId = categoryId });
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!_userManager.Users.Any(u => u.Id == userId))
            {
                return NotFound("User not found!");
            }

            var command = new UpdateCategoryCommand(
                UserId: userId,
                CategoryId: dto.CategoryId,
                Name: dto.Name
            );

            var categoryId = await _mediator.Send(command);

            return Ok(new { categoryId = categoryId });
        }

        [Authorize]
        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new DeleteCategoryCommand(
                UserId: userId,
                CategoryId: categoryId
            );

            await _mediator.Send(command);

            return Ok();
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var categories = await _mediator.Send(new GetAllCategoriesQuery(userId));
            
            return Ok(new { categories = categories });
        }

        [Authorize]
        [HttpGet("get/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var category = await _mediator.Send(new GetCategoryQuery(userId, categoryId));
            
            return Ok(new { category = category });
        }
    }
}
