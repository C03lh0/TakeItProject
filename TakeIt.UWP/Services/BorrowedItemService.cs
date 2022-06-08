
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;
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


        public async Task Add(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var completeObject = await SaveImage(obj, filesImage);
            bool saved = await _borrowedItemRepository.SaveAsync(completeObject);

            VerifyIfExecuted(saved);
        }

        public async Task Change(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var completeObject = await SaveImage(obj, filesImage);
            bool changed = await _borrowedItemRepository.UpdateAsync(completeObject);

            VerifyIfExecuted(changed);
        }

        public async Task Remove(int id, string imagePath)
        {
            bool removed = await _borrowedItemRepository.DeleteAsync(id);
            if (!imagePath.Equals("Images\\order.png") && removed)
            {
                StorageFile imageFinded = await ApplicationData.Current.LocalFolder.GetFileAsync(imagePath);
                await imageFinded.DeleteAsync();
            }
            VerifyIfExecuted(removed);
        }

        private static void VerifyIfExecuted(bool saved)
        {
            if (saved)
            {
                //Chamar Dialog Service
            }
            else
            {
                //Chamar Dialog Service
            }
        }

        private async Task<TEntity> SaveImage(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var destFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
            var destFolderPath = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");
            var imageToBeSaved = filesImage.First();
            var image = await imageToBeSaved.CopyAsync(destFolderPath, imageToBeSaved.Name, NameCollisionOption.ReplaceExisting);
            var imagePath = Path.Combine("Images", image.Name);
            obj.ImagePath = imagePath;
            return obj;
        }

        
        public async Task<TEntity> FindAsync(int id)
        {
           return await _borrowedItemRepository.FindAsync(id);
        }
        public async Task<List<TEntity>> GetList()=> await _borrowedItemRepository.ListAsync();

    }


}
