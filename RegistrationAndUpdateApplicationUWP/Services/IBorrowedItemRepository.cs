using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    interface IBorrowedItemRepository
    {
        Task SaveAsync(BorrowedItem product);
        Task UpdateAsync(BorrowedItem product);
        Task DaleteAsync(BorrowedItem product);
        Task<BorrowedItem> FindAsync(int id);
    }
}
