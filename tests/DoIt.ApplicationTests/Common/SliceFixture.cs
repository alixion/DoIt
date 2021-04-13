using System;
using System.Threading.Tasks;
using DoIt.Application;
using DoIt.Infrastructure;
using DoIt.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DoIt.ApplicationTests.Common
{
    [CollectionDefinition(nameof(SliceFixture))]
    public class SliceFixtureCollection : ICollectionFixture<SliceFixture>
    {
    }

    public class SliceFixture : IDisposable
    {
        private readonly IServiceScope _scope;
        public IServiceScope Scope => _scope;
        private readonly ServiceProvider _serviceProvider;
        private readonly DoItDbContext _context;
        private readonly IConfiguration _config;

        public SliceFixture()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton(_config);

            services
                //.AddLogging()
                .AddApplication()
                .AddInfrastructure(_config.GetConnectionString("DefaultConnection"));

            _serviceProvider = services.BuildServiceProvider();

            _scope = CreateScope();
            _context = _scope.ServiceProvider.GetService<DoItDbContext>();
        }



        public async Task<TEntity> FindAsync<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediatr = _scope.ServiceProvider.GetService<IMediator>() ??
                          throw new NullReferenceException("Could not get Mediatr from the service provider");
            return await mediatr.Send(request);
        }


        private IServiceScope CreateScope()
        {
            var serviceFactory = _serviceProvider.GetService<IServiceScopeFactory>() ??
                                 throw new NullReferenceException(
                                     "Could not get ScopeFactory from the service provider.");
            return serviceFactory.CreateScope();
        }

        public void Dispose()
        {
            _scope?.Dispose();
            _serviceProvider?.Dispose();
        }
    }
}