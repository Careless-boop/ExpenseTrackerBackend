using AutoMapper;
using ExpenseTracker.Application.DTOs.Category;
using ExpenseTracker.Application.Mediator.Categories.Commands;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Category, CreateCategoryCommand>()
                .ReverseMap();
        }
    }
}
