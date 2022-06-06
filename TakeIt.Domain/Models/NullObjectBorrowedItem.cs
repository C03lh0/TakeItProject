using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeIt.Domain.Models
{
    public class NullObjectBorrowedItem : BorrowedItem
    {
        public NullObjectBorrowedItem()
        {
            ID = -1;
            Name = "Add um novo objeto";
            Description = "Descrição do objeto";
            LoanDate = DateTimeOffset.Now;
            ReturnDate = DateTimeOffset.Now;
            ImagePath = "ms-appx:///Assets/Image/order.png";
            RegistrationDate = DateTimeOffset.Now;
        }
    }
}
