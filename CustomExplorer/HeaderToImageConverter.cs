using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomExplorer
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var node = value as ExplorerNode;
            if (node == null)
                return null;

            if (node.ExplorerType == ExplorerType.File)
            {
                return GetIconForPath(node.Path);
            }
            if (node.ExplorerType == ExplorerType.Drive)
            {
                var uri = new Uri("pack://application:,,,/Images/diskdrive.png");
                var source = new BitmapImage(uri);
                return source;
            }
            else
            {
                var uri = new Uri("pack://application:,,,/Images/folder.png");
                var source = new BitmapImage(uri);
                return source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }

        private static ImageSource GetIconForPath(string path)
        {
            if (File.Exists(path))
            {
                using (System.Drawing.Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(path))
                {
                    if (sysicon != null)
                    {
                        var icon = Imaging.CreateBitmapSourceFromHIcon(sysicon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        return icon;
                    }
                }
            }
            return null;
        }
    }
}

