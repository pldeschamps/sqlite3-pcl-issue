using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace AlmicantaratXF.Converters
{
    class SelectedItemToBool : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            int? selectedItem = value as int?;
            //string strParameter = parameter as string;
            int intParameter = int.Parse(parameter as string);
            /* selectedItems are Sun=0, Moon=1, Planets=2, Stars=3
             * pickers are Limb(Lower or Higher)=1, Planets=2, Stars=3
             *if the Sun or the Moon is selected, The Limb Picker (lower or higher) has to be visible */
            if (selectedItem == 0) selectedItem = 1; //Sun or Moon means Limb picker to be visible
            return selectedItem == intParameter;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
