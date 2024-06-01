using SQLFreshRotten.api.Interfaces;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.implements
{
    public class RecordModify<T> : ILoad<T> where T : class
    {
        private readonly DbCtx _context;

        public RecordModify(DbCtx context)
        {
            _context = context;
        }

        public async Task InsertRange(List<T> entitys)
        {
            await _context.Set<T>().AddRangeAsync(entitys);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOne(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
