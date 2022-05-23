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
            base.ID = 0;
            base.Name = "Nome do objeto";
            base.Description = "Descrição do objeto";
            base.LoanDate = DateTimeOffset.Now;
            base.ReturnDate = DateTimeOffset.Now;
            base.ImagePath = "ms-appx:///Assets/Image/order.png";
        }
    }
}
