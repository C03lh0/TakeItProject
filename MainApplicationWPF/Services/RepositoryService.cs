using MainApplicationWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplicationWPF.Services
{
    public class RepositoryService : IRepositoryService
    {
        public Task<List<BorrowedItem>> GetAllBorrowedItens()
        {
            throw new NotImplementedException();
        }
    }
}
