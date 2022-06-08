using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TakeIt.Commos;
using TakeIt.Domain.Interface;
using TakeIt.Domain.Models;
using TakeIt.Infra.Data.Repository;
using TakeIt.Services.Commands;
using TakeIt.WPF.Services;
using TakeIt.WPF.ViewModels;

namespace TakeIt.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly int _maximumItems;
        public Command OpenList { get; private set; }
        public Command RefreshWindow { get; private set; }
        public Command RegisterBorrowedItem { get; private set; }
        public List<BorrowedItem> ListBorrowedItens { get; set; }
        private readonly IBorrowedItemService<BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

        public MainViewModel()
        {
            _maximumItems = 5;
            OpenList = new Command(ShowList);
            RegisterBorrowedItem = new Command(Register);
            RefreshWindow = new Command(GetAllListBorrwedItem);
            _borrowedItemService = new BorrowedItemService<BorrowedItem>(_borrowedItemRepository, _maximumItems);
            GetAllListBorrwedItem();
        }

        public async void  GetAllListBorrwedItem()
        {
            ListBorrowedItens = await _borrowedItemService.GetList();
            
            if(ListBorrowedItens == null)
            {
                ListBorrowedItens = new List<BorrowedItem> { new NullObjectBorrowedItem() };
            }        
        }

        public void Register()
        {
            Process.Start($"com.takeituwp://?page={PageTokens.BorrowedItemFormView}");
        }

        public void ShowList()
        {
            Process.Start($"com.takeituwp://?page={PageTokens.BorrowedItemListView}");
        }
    }
}
