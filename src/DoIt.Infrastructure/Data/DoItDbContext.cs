using DoIt.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DoIt.Infrastructure.Data
{
    public class DoItDbContext:DbContext
    {
        private readonly IMediator _mediator;

        // Needed for DoItDbContextFactory
        public DoItDbContext(DbContextOptions<DoItDbContext> options):base(options)
        {}

        public DoItDbContext(DbContextOptions<DoItDbContext> options, IMediator mediator):base(options)
        {
            _mediator = mediator;
        }


        public DbSet<ToDoList> ToDoLists { get; set; }
        
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoItDbContext).Assembly);
        }
    }
}