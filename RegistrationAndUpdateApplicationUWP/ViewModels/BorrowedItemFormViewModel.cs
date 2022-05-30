using RegistrationAndUpdateApplicationUWP.Models;
using RegistrationAndUpdateApplicationUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace RegistrationAndUpdateApplicationUWP.ViewModels
{
    public class BorrowedItemFormViewModel : ViewModelBase
    {
        private BorrowedItem model;
        private AppShell _currentAppShell;
        private readonly IBorrowedItemService _borrowedItemService;

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

        public BorrowedItemFormViewModel()
        {
            model = new NullObjectBorrowedItem();
            _image = new ObservableCollection<StorageFile>();
            _borrowedItemService = new BorrowedItemService();
            Initialization();
        }

        public BorrowedItemFormViewModel(AppShell currentAppShell) : this()
        {
            _currentAppShell = currentAppShell;
        }

        private async void Initialization()
        {
            await LoadBorrowedItemAsync();
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

        public async void RegistrateOrUpdate()
        {
           ID = await _borrowedItemService.CreateAsync(ID, Name, Description, LoanDate, ReturnDate, Image);
           //CoreApplication.GetCurrentView().CoreWindow.Close();
        }

        public async void Delete()
        {
            bool deleteSucess = await _borrowedItemService.Delete(ID);
        }

        public void Cancel()
        {
            //CoreApplication.GetCurrentView().CoreWindow.Close();
        }

        private async Task LoadBorrowedItemAsync()
        {
            if(ID != 0)
            {
                model = await _borrowedItemService.FindAsync(ID);
            }
            _name = model.Name;
            _description = model.Description;
            _loanDate = model.LoanDate.Date;
            _returnDate = model.LoanDate.Date;
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
    }
}
