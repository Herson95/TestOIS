namespace TestOIS.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;
    using Interfaces;

    public class LocalImage : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = $"{value}";
            ImageSource img = null;
            var imageHelper = DependencyService.Get<IFileHelper>();
            string image = imageHelper.GetLocalFilePath(text);
            if (!string.IsNullOrEmpty(image))
            {
                img = ImageSource.FromFile(image);
                return img;
                
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}

