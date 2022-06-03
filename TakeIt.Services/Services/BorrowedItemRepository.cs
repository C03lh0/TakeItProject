using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;

namespace TakeIt.Services.Services
{
    public class BorrowedItemRepository <TEntity> : IBorrowedItemRepository <TEntity> where TEntity : BaseEntity
    {
        
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(int id)
        {
            return null;
        }

        public Task<List<TEntity>> ListAsync()
        {
            return null;
        }

        public Task<int> SaveAsync(TEntity product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity product)
        {
            int id = product.ID;
            throw new NotImplementedException();
        }
    }
}
