using System.Linq.Expressions;
using AdminTemplate.Models.Entities.Abstracts;

namespace AdminTemplate.BusinessLogic.Repository.Abstracts.MongoDb
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>
    where TEntity : BaseEntity<TKey>
    {
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(TKey id)
        {
            throw new NotImplementedException();
        }

        public TKey Insert(TEntity entity, bool isSaveLater = false)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity, bool isSaveLater = false)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity, bool isSaveLater = false)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}
