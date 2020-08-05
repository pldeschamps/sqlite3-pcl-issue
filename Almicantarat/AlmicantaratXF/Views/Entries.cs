using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlmicantaratXF.Views
{
    public class EntryNumeric : Entry
    {
        public EntryNumeric(string text)
        {
            Text = text;
            Keyboard = Keyboard.Numeric;
            FontSize = 14;
            MaxLength = 6;
            VerticalOptions = LayoutOptions.Center;
        }
    }
}