using SQLFreshRotten.api.Abstracts;
using SQLFreshRotten.api.Interfaces;
using SQLFreshRotten.api.ProviderContext;

namespace SQLFreshRotten.api.LogicProcess.implements
{
    public class EfLoadFactory : LoadFactory
    {
        private readonly DbCtx _context;

        public EfLoadFactory (DbCtx context)
        {
            _context = context;
        }

        public override ILoad<T> AddItemRange<T> ()
        {
            return new RecordModify<T>(_context);
        }

        public override ILoad<T> AddItem<T>()
        {
            return new RecordModify<T>(_context);
        }
    }
}
