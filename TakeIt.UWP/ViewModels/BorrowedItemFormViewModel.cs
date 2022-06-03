using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;
using TakeIt.Services.Services;
using TakeIt.UWP.Models;
using TakeIt.UWP.Services;
using TakeIt.UWP.Views;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace TakeIt.UWP.ViewModels
{
    public class BorrowedItemFormViewModel : ViewModelBase
    {
        private BorrowedItem model;
        private AppShell _currentAppShell;
        private readonly IBorrowedItemServiceUWP <BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();

        public int ID { get; set; }
        public string DaysLeftForReturn => ReturnDate.Subtract(LoanDate).Days.ToString();

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTimeOffset _loanDate;
        public DateTimeOffset LoanDate
        {
            get { return _loanDate; }
            set
            {
                _loanDate = value;
                OnPropertyChanged(nameof(LoanDate));
            }
        }

        private DateTimeOffset _returnDate;
        public DateTimeOffset ReturnDate
        {
            get { return _returnDate; }
            set
            {
                _returnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        private ObservableCollection<StorageFile> _image;
        public ObservableCollection<StorageFile> Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private string _imageString;
        public string ImageString
        {
            get { return _imageString; }
            set 
            {
                _imageString = value;
                OnPropertyChanged(nameof(ImageString));
            }
        }

        public BorrowedItemFormViewModel(AppShell currentAppShell, int id)
        {
            ID = id;
            _currentAppShell = currentAppShell;
            model = new NullObjectBorrowedItem();
            _image = new ObservableCollection<StorageFile>();
            _borrowedItemService = new BorrowedItemService<BorrowedItem> (_borrowedItemRepository);
            InitializeItem();
        }

        private async void InitializeItem()
        {
            await LoadBorrowedItemAsync();
        }

        private async Task LoadBorrowedItemAsync()
        {
            try
            {
                var item = await _borrowedItemService.FindAsync(ID);
                model = item;
            }
            catch
            {
                _name = model.Name;
                _description = model.Description;
                _loanDate = model.LoanDate.Date;
                _returnDate = model.LoanDate.Date;
            }
            await LoadBorrowedItemImage();
        }

        private async Task LoadBorrowedItemImage()
        {
            if (model is NullObjectBorrowedItem)
            {
                var uriNullObjectImage = new Uri(model.ImagePath);
                var find = await StorageFile.GetFileFromApplicationUriAsync(uriNullObjectImage);
                Image.Add(find);
            }
            else
            {
                var path = Path.Combine("Images", model.ImagePath);
                var finded = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                Image.Add(finded);
            }
        }

        public async void AddImage()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            Image.Clear();
            Image.Add(file);
        }

        public async void Registrate()
        {
            BorrowedItem borrowedItem = CreateBorrowedItem();
            ID = await _borrowedItemService.Add(borrowedItem, Image);
        }

        public async void Update()
        {
            BorrowedItem borrowedItem = CreateBorrowedItem();
            await _borrowedItemService.Change(borrowedItem, Image);
        }

        private BorrowedItem CreateBorrowedItem()
        {
            return new BorrowedItem()
            {
                Name = _name,
                Description = _description,
                LoanDate = _loanDate,
                ReturnDate = _returnDate,
                RegistrationDate = DateTime.Now
            };
        }

        public async void Delete()
        {
            bool deleteSucess = await _borrowedItemService.Remove(ID);
        }

        public void Cancel()
        {
            _currentAppShell.MainFrame.Navigate(typeof(BorrowedItemListView), new Object[] {_currentAppShell});
        }

    }
}
