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
        void Remove(int id);
        Task<TEntity> FindAsync(int id);
        Task Change(TEntity obj, ObservableCollection<StorageFile> filesImage);
        void Add(TEntity obj, ObservableCollection<StorageFile> filesImage);
    }
}
