using DoIt.Domain;
using DoIt.Domain.TodoListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoIt.Infrastructure.Data.Configuration
{
    public class ToDoListConfiguration:IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Title).IsRequired();
            builder.HasIndex(c => c.Title);
        }
    }
}