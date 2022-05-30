using RegistrationAndUpdateApplicationUWP.Models;
using RegistrationAndUpdateApplicationUWP.Services;
using RegistrationAndUpdateApplicationUWP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RegistrationAndUpdateApplicationUWP.ViewModels
{
    public class BorrowedItemListViewModel : ViewModelBase
    {
        private AppShell _currentAppShell;
        private readonly IBorrowedItemService _borrowedItemService;

        private ObservableCollection<BorrowedItemViewModel> items;
        public ObservableCollection<BorrowedItemViewModel> Items
        {
            get { return items; }
            set 
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public BorrowedItemListViewModel()
        {
            _borrowedItemService = new BorrowedItemService();
            BorrowedItem borrowedItem = new NullObjectBorrowedItem();
            items = new ObservableCollection<BorrowedItemViewModel>()
            {
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
                new BorrowedItemViewModel(borrowedItem),
            };
        }

        public BorrowedItemListViewModel(AppShell currentAppShell) : this()
        {
            _currentAppShell = currentAppShell;
           
            //InitializeItems();
        }

        public void OpenDetailsBorrowedItem(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem is BorrowedItem item)
            {
                var id = item.ID;
                //_currentAppShell.MainFrame.Navigate(typeof(BorrowedItemFormView), id);
            }
        }

        private async void InitializeItems()
        {
            var borrowedItems = await _borrowedItemService.GetAllBorrowedItem();

            var list = borrowedItems.Select(i => new BorrowedItemViewModel(i));

            Items = new ObservableCollection<BorrowedItemViewModel>(list);
        }
    }
}
