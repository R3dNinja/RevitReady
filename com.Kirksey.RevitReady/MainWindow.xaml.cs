using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DotNetBrowser;
using DotNetBrowser.WPF;

namespace com.Kirksey.RevitReady
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Browser browser;
        WPFBrowserView browserView;

        public MainWindow()
        {
            BrowserPreferences.SetUserAgent("Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1");
            InitializeComponent();

            browser = BrowserFactory.Create();
            browserView = new WPFBrowserView(browser);
            mainLayout.Children.Add(browserView);

            browser.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";

            browserView.Browser.LoadURL("kirksey.helpdocsonline.com");
        }
    }
}
