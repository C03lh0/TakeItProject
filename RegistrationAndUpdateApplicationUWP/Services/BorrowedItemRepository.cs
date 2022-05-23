using RegistrationAndUpdateApplicationUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Services
{
    public class BorrowedItemRepository : IBorrowedItemRepository
    {
        
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BorrowedItem> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync(BorrowedItem product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, BorrowedItem product)
        {
            throw new NotImplementedException();
        }
    }
}
