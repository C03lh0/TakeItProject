using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;


namespace TakeIt.Domain.Interface
{
    public interface IBorrowedItemRepository <TEntity> where TEntity : BaseEntity
    { 
        Task<bool> DeleteAsync(int id);
        Task<TEntity> FindAsync(int id);
        Task<List<TEntity>> ListAsync();
        Task UpdateAsync(TEntity product);
        Task<int> SaveAsync(TEntity product);
    }
}
