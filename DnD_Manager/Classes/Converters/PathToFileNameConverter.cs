using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace DnD_Manager.Classes.Converters
{
    public class PathToFileNameConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not null ? Path.GetFileNameWithoutExtension(value.ToString()) : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
