using Xamarin.Forms;

namespace AlmicantaratXF.Views
{
    class LabelTitle1 : Label
    {
        public LabelTitle1(string text)
        {
            Text = text;
            FontSize = 18;
            TextColor = Color.Fuchsia;
        }
    }
    class LabelTitle2 : Label
    {
        public LabelTitle2(string text)
        {
            Text = text;
            FontSize = 16;
            TextColor = Color.Green;
        }
    }
    class LabelParagraph : Label
    {
        public LabelParagraph(string text)
        {
            Text = text;
            FontSize = 14;
            TextColor = Color.Silver;
            VerticalOptions = LayoutOptions.Center;
        }
    }
}
