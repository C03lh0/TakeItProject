using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplicationWPF.Models
{
    public class BorrowedItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime RegistrationDate { get; set; }


        public BorrowedItem()
        {

        }

        public BorrowedItem(int id, string name, string description, DateTime loanDate, DateTime returnDate, DateTime registrationDate)
        {
            ID = id;
            Name = name;
            Description = description;
            LoanDate = loanDate;
            ReturnDate = returnDate;
            RegistrationDate = registrationDate;
        }
    }
}
