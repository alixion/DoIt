using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DoIt.Domain;
using DoIt.Domain.Common;
using DoIt.Domain.TodoListAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DoIt.Infrastructure.Data
{
    public class DoItDbContext : DbContext
    {
        private readonly IMediator _mediator;

        // Needed for DoItDbContextFactory
        public DoItDbContext(DbContextOptions<DoItDbContext> options) : base(options)
        {
        }

        public DoItDbContext(DbContextOptions<DoItDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }


        public DbSet<TodoList> ToDoLists { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoItDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }

            return result;
        }
        
        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}