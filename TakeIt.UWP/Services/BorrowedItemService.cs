
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;
using TakeIt.Services.Services;
using TakeIt.UWP.Models;
using Windows.Storage;

namespace TakeIt.UWP.Services
{
    public class BorrowedItemService <TEntity> : IBorrowedItemServiceUWP <TEntity> where TEntity : BaseEntity
    {

        private readonly IBorrowedItemRepository<TEntity> _borrowedItemRepository;

        public BorrowedItemService(IBorrowedItemRepository<TEntity> borrowedItemRepository)
        {
            _borrowedItemRepository = borrowedItemRepository;
        }


        public async Task<int> Add(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var completeObject = await SaveImage(obj, filesImage);
            try
            {
                return await _borrowedItemRepository.SaveAsync(completeObject);
            }
            catch
            {
                //Chamar Dialog Service
                return 0;
            }
        }

        public async Task Change(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var completeObject = await SaveImage(obj, filesImage);
            try
            {
                await _borrowedItemRepository.UpdateAsync(completeObject);
            }
            catch (Exception)
            {
                //Chamar Dialog Service
            }
        }

        private static async Task<TEntity> SaveImage(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var destFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
            var imageToBeSaved = filesImage.FirstOrDefault();
            var image = await imageToBeSaved.CopyAsync(destFolder, imageToBeSaved.Name, NameCollisionOption.ReplaceExisting);
            var imagePath = Path.Combine("Images", image.Name);
            obj.ImagePath = imagePath;
            return obj;
        }

        public Task<bool> Remove(int id) => _borrowedItemRepository.DeleteAsync(id);
        public Task<TEntity> FindAsync(int id) => _borrowedItemRepository.FindAsync(id);
        public async Task<List<TEntity>> GetList()=> await _borrowedItemRepository.ListAsync();
        
    }


}
