using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    public class BorrowedItemService : IBorrowedItemService
    {

        private readonly IBorrowedItemRepository _borrowedItemRepository;

        public BorrowedItemService()
        {
            _borrowedItemRepository = new BorrowedItemRepository();
        }
        
        
        public Task<BorrowedItem> CreateAsync(string name, string descrition, DateTime loanDate, DateTime returnDate, string imagePath)
        {
            throw new NotImplementedException();
        }

        public Task<BorrowedItem> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BorrowedItem> FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
