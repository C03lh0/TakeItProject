using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    public class BorrowedItemService : IBorrowedItemService
    {

        private readonly IBorrowedItemRepository _borrowedItemRepository;

        public BorrowedItemService()
        {
            _borrowedItemRepository = new BorrowedItemRepository();
        }
        
        
        public async Task<int> CreateAsync(int id, string name, string descrition, DateTimeOffset loanDate, DateTimeOffset returnDate, ObservableCollection<StorageFile> filesImage)
        {
            var destFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);

            var imageToBeSaved = filesImage.FirstOrDefault();
            var image = await imageToBeSaved.CopyAsync(destFolder, imageToBeSaved.Name, NameCollisionOption.ReplaceExisting);

            var borrowedItem = new BorrowedItem
            {
                Name = name,
                Description = descrition,
                LoanDate = loanDate,
                ReturnDate = returnDate,
                ImagePath = Path.Combine("Images", image.Name),
                RegistrationDate = DateTime.Now
            };

            if (id == 0)
            {
                borrowedItem.ID = await _borrowedItemRepository.SaveAsync(borrowedItem);
                return borrowedItem.ID;
            }
            else
            {
                await _borrowedItemRepository.UpdateAsync(id, borrowedItem);
                return id;
            }
            
        }
        public Task<bool> Delete(int id) => _borrowedItemRepository.DeleteAsync(id);
        public Task<BorrowedItem> FindAsync(int id) => _borrowedItemRepository.FindAsync(id);
        public async Task<List<BorrowedItem>> GetAllBorrowedItem()=> await _borrowedItemRepository.ListAsync();
        
    }


}
