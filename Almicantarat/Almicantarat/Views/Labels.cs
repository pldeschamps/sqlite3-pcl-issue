using Xamarin.Forms;

namespace Almicantarat.Views
{
    class LabelTitle1 : Label
    {
        public LabelTitle1(string text)
        {
            Text = text;
            FontSize = 32;
            TextColor = Color.Fuchsia;
        }
    }
    class LabelTitle2 : Label
    {
        public LabelTitle2(string text)
        {
            Text = text;
            FontSize = 22;
            TextColor = Color.Green;
        }
    }
    class LabelParagraph : Label
    {
        public LabelParagraph(string text)
        {
            Text = text;
            FontSize = 12;
            TextColor = Color.Silver;
        }
    }
}
