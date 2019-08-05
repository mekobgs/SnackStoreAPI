using Microsoft.EntityFrameworkCore;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SnackStoreDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly IEventDispatcher _domainEventsDispatcher;

        public Repository(SnackStoreDbContext context, IEventDispatcher domainEventsDispatcher)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet;
        }

        public IQueryable<T> FindByExpression(Expression<Func<T, bool>> expression)
        {
            return _dbSet
                .Where(expression);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Edit(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await ExecuteDomainEvents();
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private async Task ExecuteDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await _domainEventsDispatcher.Dispatch(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
