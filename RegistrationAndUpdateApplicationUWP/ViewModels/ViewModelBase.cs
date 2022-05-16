using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndUpdateApplicationUWP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propetyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propetyName));
        }
    }
}
