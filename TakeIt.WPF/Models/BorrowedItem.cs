using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;

namespace TakeIt.Models
{
    public class BorrowedItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
