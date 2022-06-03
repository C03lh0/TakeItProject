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
using TakeIt.Models;
using TakeIt.Services.Commands;
using TakeIt.Services.Services;
using TakeIt.WPF.Models;
using TakeIt.WPF.Services;
using TakeIt.WPF.ViewModels;

namespace TakeIt.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Command OpenList { get; private set; }
        public Command RefreshWindow { get; private set; }
        public Command RegisterBorrowedItem { get; private set; }
        public List<BorrowedItem> ListBorrowedItens { get; set; }
        private IBorrowedItemService<BorrowedItem> _borrowedItemService;
        private IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();

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
            OpenList = new Command(ShowList);
            RegisterBorrowedItem = new Command(Register);
            RefreshWindow = new Command(GetAllListBorrwedItem);
            _borrowedItemService = new BorrowedItemService<BorrowedItem>(_borrowedItemRepository);
            ListBorrowedItens = new List<BorrowedItem>();
            GetAllListBorrwedItem();
        }

        public async void  GetAllListBorrwedItem()
        {
            try
            {
                ListBorrowedItens.Clear();
                ListBorrowedItens = await _borrowedItemService.GetList();
            }
            catch(Exception)
            {
                ListBorrowedItens.Clear();
                var nullItem = new NullObjectBorrowedItem();
                ListBorrowedItens.Add(nullItem);
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
