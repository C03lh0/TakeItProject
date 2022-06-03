using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;

namespace TakeIt.WPF.Services
{
    public class BorrowedItemService<TEntity> : IBorrowedItemService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBorrowedItemRepository<TEntity> _borrowedItemRepository;

        public BorrowedItemService(IBorrowedItemRepository<TEntity> borrowedItemRepository)
        {
            _borrowedItemRepository = borrowedItemRepository;
        }

        public Task<List<TEntity>> GetList()
        {
            return _borrowedItemRepository.ListAsync();
        }
    }
}
