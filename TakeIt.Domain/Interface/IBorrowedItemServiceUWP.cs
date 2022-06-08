using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using Windows.Storage;

namespace TakeIt.Domain.Interface
{
    public interface IBorrowedItemServiceUWP <TEntity> : IBorrowedItemService <TEntity> where TEntity : BaseEntity
    {
        Task Remove(int id);
        Task<TEntity> FindAsync(int id);
        Task Change(TEntity obj, ObservableCollection<StorageFile> filesImage);
        Task Add(TEntity obj, ObservableCollection<StorageFile> filesImage);
    }
}
