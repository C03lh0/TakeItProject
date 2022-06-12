using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Domain.Entities;

namespace TakeIt.Domain.Models
{
    public class BorrowedItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset LoanDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}
