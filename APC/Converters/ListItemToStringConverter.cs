using APC.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace APC.Converters
{
    public class ListItemToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null)
            {
                return "Готово";
            }
            if (value is Cars)
            {
                result = $"Выбран автомобиль: {((Cars)value).CarMark} с номером {((Cars)value).Number}";
            }
            else if (value is Departure)
            {
                result = $"Выбран автомобиль: {((Departure)value).CarMark} с номером {((Departure)value).Number}";
            }
            else if (value is Repair)
            {
                result = $"Выбран автомобиль: {((Repair)value).CarMark} с номером {((Repair)value).Number}";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Готово";
        }
    }
}
