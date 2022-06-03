using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeIt.Models;

namespace TakeIt.WPF.Models
{
    public class NullObjectBorrowedItem : BorrowedItem
    {
        public NullObjectBorrowedItem()
        {
            ID = 0;
            Name = "Nome do objeto";
            Description = "Descrição do objeto";
            LoanDate = DateTime.Now;
            ReturnDate = DateTime.Now;
            ImagePath = "Assets/order.png";
            RegistrationDate = DateTime.Now;
        }
    }
}
