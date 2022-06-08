using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;
using TakeIt.Domain.Interface;
using TakeIt.Domain.Models;
using TakeIt.Infra.Data.Repository;
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
        private readonly AppShell _currentAppShell;
        private readonly IBorrowedItemServiceUWP <BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();

        public int ID { get; set; }
        public string ImagePath { get; private set; }
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
            var findedItem = await _borrowedItemService.FindAsync(ID);
            if (findedItem != null) 
            {
                model = findedItem;
            }
            _name = model.Name;
            _description = model.Description;
            _loanDate = model.LoanDate;
            _returnDate = model.ReturnDate;
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
                ImagePath = model.ImagePath; // Path.Combine("Images", );
                StorageFile finded = await ApplicationData.Current.LocalFolder.GetFileAsync(ImagePath);
                Image.Clear();
                Image.Add(finded);
            }
        }

        public async void AddImage()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();

            if(file != null)
            {
                Image.Clear();
                Image.Add(file);
            }
            else
            {
                Image.Clear();
                await LoadBorrowedItemImage();
            }
            
        }

        public async void Registrate()
        {
            BorrowedItem borrowedItem = CreateBorrowedItem();
            await _borrowedItemService.Add(borrowedItem, Image);
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
           await _borrowedItemService.Remove(ID, ImagePath);
        }

        public void Cancel()
        {
            _currentAppShell.MainFrame.Navigate(typeof(BorrowedItemListView), new Object[] {_currentAppShell});
        }

    }
}
