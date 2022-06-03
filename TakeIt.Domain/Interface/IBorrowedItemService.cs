using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using Windows.Storage;

namespace TakeIt.Domain.Interface
{
    public interface IBorrowedItemService <TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetList();
    }
}
