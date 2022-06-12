
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

        public async Task<bool> Add(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            StorageFile imageDefault = await ApplicationData.Current.LocalFolder.GetFileAsync("Images\\order.png");
            var completeObject = obj;
            completeObject.ImagePath = "Images\\order.png";
            if (!(filesImage.ElementAt(0).Name.Equals("order.png")))
            {
               completeObject = await SaveImage(obj, filesImage);
            }
            bool saved = await _borrowedItemRepository.SaveAsync(completeObject);
            return saved;
        }

        public async Task<bool> Change(TEntity obj, ObservableCollection<StorageFile> currentImage, string imageBeforePath, int id)
        {
            StorageFile imageFinded = await ApplicationData.Current.LocalFolder.GetFileAsync(imageBeforePath);
            var completeObject = obj;
            completeObject.ImagePath = imageBeforePath;
            completeObject.ID = id;
            if (!(imageFinded.Name.Equals(currentImage.ElementAt(0).Name)))
            {
                await DeleteImage(imageBeforePath);
                completeObject = await SaveImage(obj, currentImage);
            }
            bool updateItem = await _borrowedItemRepository.UpdateAsync(completeObject);
            return updateItem;
        }

        public async Task<bool> Remove(int id, string imagePath)
        {
            bool removed = await _borrowedItemRepository.DeleteAsync(id);
            if(removed)
                await DeleteImage(imagePath);

            return removed;
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _borrowedItemRepository.FindAsync(id);
        }

        private static async Task DeleteImage(string imagePath)
        {
            if (!imagePath.Equals("Images\\order.png"))
            {
                StorageFile imageFinded = await ApplicationData.Current.LocalFolder.GetFileAsync(imagePath);
                await imageFinded.DeleteAsync();
            }
        }

        private async Task<TEntity> SaveImage(TEntity obj, ObservableCollection<StorageFile> filesImage)
        {
            var destFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
            var destFolderPath = await ApplicationData.Current.LocalFolder.GetFolderAsync("Images");
            var imageToBeSaved = filesImage.First();
            var image = await imageToBeSaved.CopyAsync(destFolderPath, imageToBeSaved.Name, NameCollisionOption.GenerateUniqueName);
            var imagePath = Path.Combine("Images", image.Name);
            obj.ImagePath = imagePath;
            return obj;
        }

        public async Task<List<TEntity>> GetList()=> await _borrowedItemRepository.ListAsync();

    }


}
