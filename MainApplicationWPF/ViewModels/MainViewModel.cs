using MainApplicationWPF.Models;
using MainApplicationWPF.Services;
using MainApplicationWPF.Services.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainApplicationWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand OpenList { get; set; }
        public RelayCommand RefreshWindow { get; set; }
        public RelayCommand RegisterOrUpdateItemToList { get; set; }
        private IRepositoryService repositoryService;
        public List<BorrowedItem> ListBorrowedItens { get; set; }


        private string _textTest;
        public string TextTeste
        {
            get { return _textTest; }
            set { OnPropertyChanged(nameof(_textTest)); }
        }

        public MainViewModel()
        {
            OpenList = new RelayCommand(ShowList);
            RefreshWindow = new RelayCommand(GetAllListBorrwedItem);
            RegisterOrUpdateItemToList = new RelayCommand(RegisterAndUpdateItem);
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
            _textTest = "Sobre";
        }



    }
}
