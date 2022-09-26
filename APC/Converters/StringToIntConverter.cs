using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace APC.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            if (value != null)
            {
                try
                {
                    result = int.Parse(value.ToString());
                    return result;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
            else if (value == null)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                MessageBox.Show("Поле не может быть пустым или состоять только из пробелов");
                return 0;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value.ToString();
            return result;
        }
    }
}
