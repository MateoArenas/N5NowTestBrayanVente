using Microsoft.EntityFrameworkCore;
using N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces;
using N5NowTestBrayanVente.Infrastructure.Contexts;

namespace N5NowTestBrayanVente.Infrastructure.Repositories
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly N5NowTestDBContext _n5NowTestDBContext;

        public GeneralRepository(N5NowTestDBContext n5NowTestDBContext)
        {
            _n5NowTestDBContext = n5NowTestDBContext;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await _n5NowTestDBContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync<T>(int Id) where T : class
        {
            return await _n5NowTestDBContext.Set<T>().FindAsync(Id);
        }

        public async Task<T> InsertAsync<T>(T entity) where T : class
        {
            var entityResult = await _n5NowTestDBContext.AddAsync<T>(entity);
            await _n5NowTestDBContext.SaveChangesAsync();

            return entityResult.Entity;
        }

        public async Task<T> UpdateAsync<T>(int Id, T entity) where T : class
        {
            var findEntity = await _n5NowTestDBContext.Set<T>().FindAsync(Id);

            if (findEntity == null)
                return null;

            var entityResult = _n5NowTestDBContext.Set<T>().Update(entity);
            await _n5NowTestDBContext.SaveChangesAsync();

            return entityResult.Entity;
        }
    }
}
