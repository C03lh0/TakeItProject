using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TakeIt.UWP.Converters
{
    public class StorageFileImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is StorageFile file)
            {
                using(var stream = file.OpenReadAsync().AsTask().Result)
                {
                    var bi = new BitmapImage();
                    bi.SetSource(stream);
                    return bi;
                }
            }

            var bil = new BitmapImage(new Uri("ms-appx:///Assets/Image/order.png"));

            return bil;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
