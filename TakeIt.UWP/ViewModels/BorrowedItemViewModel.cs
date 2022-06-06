using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Models;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace TakeIt.UWP.ViewModels
{
    public class BorrowedItemViewModel : ViewModelBase
    {
        public int ID { get; set; }
        private string imagePath;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string loanDate;
        public string LoanDate
        {
            get { return loanDate; }
            set
            {
                loanDate = value;
                OnPropertyChanged(nameof(LoanDate));
            }
        }

        private string returnDate;
        public string ReturnDate
        {
            get { return returnDate; }
            set
            {
                returnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        private string registrationDate;
        public string RegistrationDate
        {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
                OnPropertyChanged(nameof(RegistrationDate));
            }
        }

        private BitmapImage thumbnail;
        public BitmapImage Thumbnail
        {
            get { return thumbnail; }
            set 
            {
                thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail)); 
            }
        }

        public BorrowedItemViewModel(BorrowedItem borrowedItem)
        {
            ID = borrowedItem.ID;
            name = borrowedItem.Name;
            description = borrowedItem.Description;
            loanDate = borrowedItem.LoanDate.ToString("d");
            returnDate = borrowedItem.ReturnDate.ToString("d");
            registrationDate = borrowedItem.RegistrationDate.ToString("D");
            imagePath = borrowedItem.ImagePath;
        }

        public async void LoadImage()
        {
            //var file = await ApplicationData.Current.LocalFolder.GetFileAsync(imagePath);
            var uriNullObjectImage = new Uri(imagePath);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uriNullObjectImage);

            using (var stream = file.OpenReadAsync().AsTask().Result)
            {
                var bi = new BitmapImage();
                bi.SetSource(stream);
                Thumbnail = bi;
            }
        }
    }
}
