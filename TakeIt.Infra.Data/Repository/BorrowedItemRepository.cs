using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;
using TakeIt.Infra.Data.Context;

namespace TakeIt.Infra.Data.Repository
{
    public class BorrowedItemRepository <TEntity> : IBorrowedItemRepository <TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext applicationContext;

        public BorrowedItemRepository()
        {
            applicationContext = new ApplicationContext();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity entity = await FindAsync(id);
            if(entity != null)
            {
                applicationContext.Set<TEntity>().Remove(entity);
                applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<TEntity> FindAsync(int id)
        {
            TEntity finded;
            try
            {
                finded = applicationContext.Set<TEntity>().Find(id);
            }
            catch (Exception)
            {
                finded = null;
            }
            return finded;
        }

        public async Task<bool> SaveAsync(TEntity product)
        {
            try
            {
                applicationContext.Set<TEntity>().Add(product);
                applicationContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity product)
        {
            try
            {
                applicationContext.Set<TEntity>().Update(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<TEntity>> ToListAsyncMaxItem(int quantity)
        {
            var listItems = await ListAsync();

            List<TEntity> listMax = new List<TEntity>();
            if (listItems != null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    listMax.Add((TEntity)listItems.ToArray().GetValue(i));
                }
            } else
            {
                return null;
            }
            return listMax;
        }

        public async Task<List<TEntity>> ListAsync() => applicationContext.Set<TEntity>().ToList();
    }
}
