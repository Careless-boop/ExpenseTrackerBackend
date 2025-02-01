using AutoMapper;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _repositoryWrapper.CategoryRepository.AddAsync(category);

            await _repositoryWrapper.SaveAsync();

            return category.Id;
        }
    }
}
