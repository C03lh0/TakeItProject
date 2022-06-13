using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TakeIt.Commos;
using TakeIt.Domain.Models;
using TakeIt.ViewModels;

namespace TakeIt.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemSelected = (sender as ListView).SelectedItem;
            if (itemSelected is BorrowedItem item)
            {
                Process.Start($"com.takeituwp://?page={PageTokens.BorrowedItemFormView}&id={item.ID}");
            }
        }
    }
}
