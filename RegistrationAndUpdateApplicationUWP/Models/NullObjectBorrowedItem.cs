using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.Models
{
    public class NullObjectBorrowedItem : BorrowedItem
    {
        public NullObjectBorrowedItem()
        {
            ID = 0;
            Name = "Nome do objeto";
            Description = "Descrição do objeto";
            LoanDate = DateTimeOffset.Now;
            ReturnDate = DateTimeOffset.Now;
            ImagePath = "ms-appx:///Assets/Image/order.png";
            RegistrationDate = DateTimeOffset.Now;
        }
    }
}
