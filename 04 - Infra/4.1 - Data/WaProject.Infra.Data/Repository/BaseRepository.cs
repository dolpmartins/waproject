using System.Collections.Generic;
using System.Linq;
using WaProject.Domain.Entities;
using WaProject.Domain.Interfaces;
using WaProject.Infra.Data.Context;

namespace WaProject.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Insert(TEntity obj)
        {
            _dataContext.Set<TEntity>().Add(obj);
            _dataContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dataContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dataContext.Set<TEntity>().Remove(Select(id));
            _dataContext.SaveChanges();
        }

        public IList<TEntity> Select() =>
            _dataContext.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _dataContext.Set<TEntity>().Find(id);

    }
}
