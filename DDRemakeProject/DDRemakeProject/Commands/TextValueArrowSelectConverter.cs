using System;
using System.Globalization;
using System.Windows.Data;

namespace DDRemakeProject.Commands
{
    public class TextValueArrowSelectConverter :IMultiValueConverter
    {
       /* public object Convert(object[] values)
         {
           return values.Clone();
         }*/


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException();
        }
    }
}