using Xamarin.Forms;

namespace Almicantarat.Views
{
    class LessonTabPage : TabbedPage
    {
        public LessonTabPage()
        {
            this.Title = "Lessons";
            this.Children.Add(new LessonPage());
            this.Children.Add(new QuizzPage());
            this.Children.Add(new LessonPage());
            this.Children.Add(new QuizzPage());
        }
    }
}
