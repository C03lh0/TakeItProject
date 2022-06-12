using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Interface;
using TakeIt.Domain.Models;
using TakeIt.Infra.Data.Repository;
using TakeIt.UWP.Services;
using TakeIt.UWP.Views;
using Windows.UI.Xaml.Controls;

namespace TakeIt.UWP.ViewModels
{
    public class BorrowedItemListViewModel : ViewModelBase
    {
        private AppShell _currentAppShell;
        private readonly IBorrowedItemServiceUWP <BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();

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

        public BorrowedItemListViewModel(AppShell currentAppShell)
        {
            _currentAppShell = currentAppShell;
            _borrowedItemService = new BorrowedItemService<BorrowedItem>(_borrowedItemRepository);
            InitializeItems();

        }

        public void OpenDetailsBorrowedItem(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem is BorrowedItemViewModel item)
            {
                _currentAppShell.MainFrame.Navigate(typeof(BorrowedItemFormView), new Object[] {_currentAppShell, item.ID});
            }
        }

        public void AddNewBorrowedItem()
        {  
           _currentAppShell.MainFrame.Navigate(typeof(BorrowedItemFormView), new Object[] { _currentAppShell, -1 });
        }

        private async void InitializeItems()
        {
            var borrowedItems = await _borrowedItemService.GetList();
            if (borrowedItems.Count != 0)
            {
                var list = borrowedItems.Select(i => new BorrowedItemViewModel(i));
                items = new ObservableCollection<BorrowedItemViewModel>(list);
            }
            else
            {
                items = new ObservableCollection<BorrowedItemViewModel>();
                var item = new BorrowedItemViewModel(new NullObjectBorrowedItem());
                items.Add(item);
            }
        }
    }
}
