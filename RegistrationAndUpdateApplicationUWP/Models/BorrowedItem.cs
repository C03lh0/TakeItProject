using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Models
{
    public class BorrowedItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset LoanDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public string ImagePath { get; set; }
    }
}
