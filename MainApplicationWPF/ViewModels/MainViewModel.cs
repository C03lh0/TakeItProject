using MainApplicationWPF.Models;
using MainApplicationWPF.Services;
using MainApplicationWPF.Services.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainApplicationWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command OpenList { get; private set; }
        public Command RefreshWindow { get; private set; }
        public Command RegisterOrUpdateItemToList { get; private set; }
        private IRepositoryService repositoryService;
        public List<BorrowedItem> ListBorrowedItens { get; set; }

        public MainViewModel()
        {
            OpenList = new Command(ShowList);
            RefreshWindow = new Command(GetAllListBorrwedItem);
            RegisterOrUpdateItemToList = new Command(RegisterAndUpdateItem);
            repositoryService = new RepositoryService();
        }

        public async void  GetAllListBorrwedItem()
        {
            ListBorrowedItens = await repositoryService.GetAllBorrowedItens();
        }

        public void RegisterAndUpdateItem()
        {
            //ListBorrowedItens = await repositoryService.GetAllBorrowedItens();
        }

        public void ShowList()
        {
            
        }



    }
}
