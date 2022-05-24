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
    public interface IBorrowedItemRepository
    {
        Task<int> SaveAsync(BorrowedItem product);
        Task UpdateAsync(int id, BorrowedItem product);
        Task<bool> DeleteAsync(int id);
        Task<BorrowedItem> FindAsync(int id);
        Task<List<BorrowedItem>> ListAsync();
    }
}
