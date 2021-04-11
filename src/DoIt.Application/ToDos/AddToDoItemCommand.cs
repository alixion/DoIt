using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DoIt.Domain;
using DoIt.Domain.Common;
using MediatR;

namespace DoIt.Application.ToDos
{
    public class AddToDoItemCommand:IRequest<ToDoItemDto>
    {
        public AddToDoItemCommand(string title)
        {
            
            Title = title;
        }

        public Guid ListId { get; set; }
        public string Title { get; }
    }
    
    public class AddToDoItemCommandHandler:IRequestHandler<AddToDoItemCommand,ToDoItemDto>
    {
        private readonly IRepository<ToDoList, Guid> _repository;
        private readonly IMapper _mapper;

        public AddToDoItemCommandHandler(IRepository<ToDoList,Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoItemDto> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetByIdAsync(request.ListId);
            var todoId = list.AddToDo(request.Title);

            await _repository.UpdateAsync(list);

            return _mapper.Map<ToDoItemDto>(list.Items.Single(x => x.Id == todoId));
        }
    }
}