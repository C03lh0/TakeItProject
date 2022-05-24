using RegistrationAndUpdateApplicationUWP.Models;
using RegistrationAndUpdateApplicationUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.ViewModels
{
    public class BorrowedItemListViewModel : ViewModelBase
    {
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
                new BorrowedItemViewModel(borrowedItem)
            };
            
            //items.Add()
            //InitializeItens();
        }

        /*private async void InitializeItens()
        {
            var borrowedItems = await _borrowedItemService.GetAllBorrowedItem();

            var list = borrowedItems.Select(i => new BorrowedItemViewModel(i));

            Items = new ObservableCollection<BorrowedItemViewModel>(list);
        }*/
    }
}
