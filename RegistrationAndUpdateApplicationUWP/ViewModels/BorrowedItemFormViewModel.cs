using RegistrationAndUpdateApplicationUWP.Models;
using RegistrationAndUpdateApplicationUWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.ViewModels
{
    public class BorrowedItemFormViewModel : ViewModelBase
    {
        private BorrowedItem model;
        private readonly IBorrowedItemService borrowedItemService;


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

        private DateTime _loanDate;
        public DateTime LoanDate
        {
            get { return _loanDate; }
            set
            {
                _loanDate = value;
                OnPropertyChanged(nameof(LoanDate));
            }
        }

        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set
            {
                _returnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public BorrowedItemFormViewModel()
        {
            borrowedItemService = new BorrowedItemService();
        }

        public async void AddImage()
        {
           
        }

        public void RegistrateOrSave()
        {

        }

        public async void Delete()
        {

        }

        public void Cancel()
        {

        }

        private async Task LoadBorrowedItemAsync(int id)
        {
            if(id != 0)
            {
                model = await borrowedItemService.FindAsync(id);

                Name = model.Name;
                Description = model.Description;
                LoanDate = model.LoanDate.Date;
                ReturnDate = model.LoanDate.Date;
                ImagePath = model.ImagePath;
            }
            else
            {

            }
            

        }
    }
}
