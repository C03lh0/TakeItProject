using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    public interface IBorrowedItemService
    {
        Task<BorrowedItem> FindAsync(int id);
        Task<BorrowedItem> Delete(int id);
        Task<BorrowedItem> CreateAsync(string name, string descrition, DateTime loanDate, DateTime returnDate, string imagePath);
    }
}
