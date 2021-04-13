using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DoIt.Domain;
using DoIt.Domain.Common;
using MediatR;

namespace DoIt.Application.ToDos
{
    public class AddToDoListCommand:IRequest<ToDoListDto>
    {
        public AddToDoListCommand(string title)
        {
            Title = title;
        }

        public string Title { get;  }
    }
    
    public class AddToDoListCommandHandler:IRequestHandler<AddToDoListCommand,ToDoListDto>
    {
        private readonly IRepository<ToDoList, Guid> _repository;
        private readonly IMapper _mapper;

        public AddToDoListCommandHandler(IRepository<ToDoList,Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoListDto> Handle(AddToDoListCommand request, CancellationToken cancellationToken)
        {
            var list = new ToDoList(request.Title);
            var createdItem = await _repository.AddAsync(list);
            
            return _mapper.Map<ToDoListDto>(createdItem);
        }
    }
}