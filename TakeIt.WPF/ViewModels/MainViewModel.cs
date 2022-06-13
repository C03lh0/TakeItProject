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
        public Command AddItem { get; private set; }
        public Command OpenList { get; private set; }
        public Command RefreshWindow { get; private set; }
        public ObservableCollection<BorrowedItem> ListBorrowedItens { get; set; }
        private readonly IBorrowedItemService<BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository;

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
            AddItem = new Command(AddBorrowedITem);
            OpenList = new Command(ShowListBorrowedItem);
            RefreshWindow = new Command(GetAllListBorrwedItem);
            ListBorrowedItens = new ObservableCollection<BorrowedItem>();
            _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();
            _borrowedItemService = new BorrowedItemServiceWPF<BorrowedItem>(_borrowedItemRepository, _maximumItems);
            GetAllListBorrwedItem();
        }

        public async void  GetAllListBorrwedItem()
        {
            var list = await _borrowedItemService.GetList();
            if (list != null)
            {
                ListBorrowedItens.Clear();
                foreach (var item in list)
                {
                    ListBorrowedItens.Add(item);
                }
            }
            else
            {
                ListBorrowedItens.Clear();
                ListBorrowedItens.Add(new NullObjectBorrowedItem());
            }
        }

        public void AddBorrowedITem()
        {
            Process.Start($"com.takeituwp://?page={PageTokens.BorrowedItemFormView}");
        }

        public void ShowListBorrowedItem()
        {
            Process.Start($"com.takeituwp://?page={PageTokens.BorrowedItemListView}");
        }
    }
}
