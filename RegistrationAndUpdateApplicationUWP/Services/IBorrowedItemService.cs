using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    public interface IBorrowedItemService
    {
        Task<BorrowedItem> FindAsync(int id);
        Task<List<BorrowedItem>> GetAllBorrowedItem();
        Task<bool> Delete(int id);
        Task<int> CreateAsync(int id, string name, string descrition, DateTimeOffset loanDate, DateTimeOffset returnDate, ObservableCollection<StorageFile> imagePath);
    }
}
