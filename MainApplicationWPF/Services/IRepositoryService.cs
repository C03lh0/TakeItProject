using MainApplicationWPF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainApplicationWPF.Services
{
    public interface IRepositoryService
    {
        Task<List<BorrowedItem>> GetAllBorrowedItens();
    }
}