using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gameBall
{
    class CompareTwoStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string str1 = (values[0] as string);
            string str2 = (values[1] as string);

            if (str1.Equals(str2) || str1.Equals("-") || str2.Equals("-"))
                return false;
            else
                return true;  
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
