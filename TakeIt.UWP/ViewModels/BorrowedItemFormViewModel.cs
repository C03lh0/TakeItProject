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
using Windows.UI.Xaml.Controls;

namespace TakeIt.UWP.ViewModels
{
    public class BorrowedItemFormViewModel : ViewModelBase
    {
        private BorrowedItem model;
        private readonly AppShell _currentAppShell;
        private readonly DialogService dialogService;
        private readonly IBorrowedItemServiceUWP <BorrowedItem> _borrowedItemService;
        private readonly IBorrowedItemRepository<BorrowedItem> _borrowedItemRepository;

        public int ID { get; set; }
        public string ImageBefore { get; set; }
        public string ImagePath { get; private set; }
        public string DaysLeftForReturn => ReturnDate.Subtract(DateTime.Now.Date).Days.ToString();

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

        private ObservableCollection<StorageFile> _currentImage;
        public ObservableCollection<StorageFile> CurrentImage
        {
            get { return _currentImage; }
            set
            {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
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
            dialogService = new DialogService(Cancel);
            _currentImage = new ObservableCollection<StorageFile>();
            _borrowedItemRepository = new BorrowedItemRepository<BorrowedItem>();
            _borrowedItemService = new BorrowedItemService<BorrowedItem> (_borrowedItemRepository);
            InitializeItem();
        }

        private async void InitializeItem()
        {
            await LoadBorrowedItemAsync();
            ReturnAlert();
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
            ImagePath = model.ImagePath;
            ImageBefore = ImagePath;
            StorageFile findedImage = await ApplicationData.Current.LocalFolder.GetFileAsync(ImagePath);
            CurrentImage.Clear();
            CurrentImage.Add(findedImage);
        }

        public async void AddImage()
        {
            var imageBefore = CurrentImage.ElementAt(0);
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var imageFile = await picker.PickSingleFileAsync();

            if(imageFile != null)
            {
                CurrentImage.Clear();
                CurrentImage.Add(imageFile);
            }
            else
            {
                await LoadBorrowedItemImage();
            }
        }

        public async void Registrate()
        {
            BorrowedItem borrowedItem = CreateBorrowedItem();
            bool saved = await _borrowedItemService.Add(borrowedItem, CurrentImage);
            if (saved)
            {
                dialogService.DispalyMessageSuccessfullyDialog("Objeto salvo com sucesso!");
            }
            else
            {
                dialogService.DispalyMessageErroDialog("Ops...Algo deu errado! Objeto não salvo!");
            }
        }

        public async void Update()
        {
            BorrowedItem borrowedItem = CreateBorrowedItem();
            var result = await dialogService.DisplayConfirmationDialog("Tem certeza que deseja atualizar este objeto?");
            if (result == ContentDialogResult.Primary)
            {
               bool updated = await _borrowedItemService.Change(borrowedItem, CurrentImage, ImageBefore, ID);
                if (updated)
                {
                    dialogService.DispalyMessageSuccessfullyDialog("Objeto atualizado com sucesso!");
                }
                else
                {
                    dialogService.DispalyMessageSuccessfullyDialog("Ops...Algo deu errado! Objeto não atualizado!");
                }
            }
        }

        public async void Delete()
        {
            var result = await dialogService.DisplayConfirmationDialog("Tem certeza que deseja deletar este objeto?");
            if (result == ContentDialogResult.Primary)
            {
                bool deleted = await _borrowedItemService.Remove(ID, ImagePath);
                if (deleted)
                {
                    dialogService.DispalyMessageSuccessfullyDialog("Objeto deletado com sucesso!");
                }
                else
                {
                    dialogService.DispalyMessageSuccessfullyDialog("Ops...Algo deu errado! Objeto não deletado!");
                }
            }
        }

        public void Cancel()
        {
            _currentAppShell.MainFrame.Navigate(typeof(BorrowedItemListView), new Object[] {_currentAppShell});
        }

        private void ReturnAlert()
        {
            if (DaysLeftForReturn.ToString().Equals("0") && ID != -1)
            {
                dialogService.DispalyMessageReturnAlertDialog("Hoje é a data limite para devolução do objeto. Cuidado pra não levar fumo!");
            }
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

    }
}
