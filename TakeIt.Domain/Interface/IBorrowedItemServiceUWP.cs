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
        Task<bool> Remove(int id, string imagePath);
        Task<TEntity> FindAsync(int id);
        Task<bool> Change(TEntity obj, ObservableCollection<StorageFile> filesImage, string imageBeforePath, int id);
        Task<bool> Add(TEntity obj, ObservableCollection<StorageFile> filesImage);
    }
}
