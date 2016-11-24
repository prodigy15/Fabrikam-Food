using Xamarin.Forms;

namespace Fabrikam_Food
{
    internal class SmartHelperPage : ContentPage
    {
        public SmartHelperPage()
        {
            var browser = new WebView();
            var source = new HtmlWebViewSource();
            source.Html = @"<html><body>
            <p>Welcome to Smart Helper ! </p>
            <iframe src='https://webchat.botframework.com/embed/SmartHelper?s=YzJL3hRFmIk.cwA.UC0.meMnMMy2DVZ-u3w8RWiJfBv9m4t9XfL4tk4ZPqtGP7s' 
            style='height:380px;width:302px'>
            </body></html>";
            browser.Source = source;

            Content = browser;
        }
    }
}