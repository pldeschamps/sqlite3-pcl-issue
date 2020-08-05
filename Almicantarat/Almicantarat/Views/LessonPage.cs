using Xamarin.Forms;

namespace Almicantarat
{
    public interface IBaseUrl { string Get(); }
    // required temporarily for iOS, due to BaseUrl bug
    // see : https://github.com/xamarin/xamarin-forms-samples/blob/master/WorkingWithWebview/WorkingWithWebview/LocalHtmlBaseUrl.cs
    public class BaseUrlWebView : WebView { }
    class LessonPage:ContentPage
    {
        public LessonPage()
        {
            Label title = new Label
            {
                Text = "Theory",
                FontSize = 40
            };

            BaseUrlWebView lessonWebview = new BaseUrlWebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html>
<head></head>
<body>
<h1>Xamarin.Forms</h1>
<p>The CSS and image are loaded from local files!</p>
<p><a href=""theory_fr.html"">next page</a></p>
</body>
</html>";

            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();


            lessonWebview.Source = htmlSource;

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                    title,
                    lessonWebview
                }
            };
            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = stackLayout
            };
            this.Content = lessonWebview;
        }

    }
}
