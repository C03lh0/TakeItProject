using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TakeIt.Commos;
using TakeIt.UWP.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TakeIt.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!string.IsNullOrEmpty(e.ToString()))
            {
                if (((Object[])e.Parameter)[0].Equals(PageTokens.BorrowedItemFormView))
                {
                    MainFrame.Navigate(typeof(BorrowedItemFormView), new Object[] {this, ((Object[])e.Parameter)[1]});
                }
                else
                {
                    MainFrame.Navigate(typeof(BorrowedItemListView), new Object[] {this});
                }
            }
        }
    }
}
