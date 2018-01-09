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

using Autodesk.Revit.UI;

using DotNetBrowser;
using DotNetBrowser.WPF;

using com.Kirksey.RevitReady.RevitItems;

namespace com.Kirksey.RevitReady.Interface
{
    /// <summary>
    /// Interaction logic for DockPanel.xaml
    /// </summary>
    public partial class DockPanel : UserControl, IDockablePaneProvider
    {
        private ExternalEvent m_ExEvent;
        private RequestHandler m_Handler;

        BrowserView webView;

        public DockPanel()
        {        
            InitializeComponent();
            setupBrowser();
        }

        public DockPanel(ExternalEvent exEvent, RequestHandler handler)
        {
            LoggerProvider.Instance.LoggingEnabled = true;
            LoggerProvider.Instance.FileLoggingEnabled = true;
            LoggerProvider.Instance.OutputFile = "H:/DotNetBrowser.log";

            BrowserPreferences.SetUserAgent("Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1");
            BrowserPreferences.SetChromiumSwitches("--lang=en");
            InitializeComponent();
            m_ExEvent = exEvent;
            m_Handler = handler;
            //setupBrowser();

            browserView.Browser.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
            browserView.Browser.LoadURL("https://kirksey.helpdocsonline.com");
            //mainLayout.Children.Add((UIElement)webView.GetComponent());
            //webView.Browser.UserAgent = "Mozilla / 5.0(iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit / 603.1.30(KHTML, like Gecko) Version / 10.0 Mobile / 14E304 Safari / 602.1";
            //webView.Browser.LoadURL("https://kirksey.helpdocsonline.com");
        }

        private void setupBrowser()
        {
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.InitialState.DockPosition = DockPosition.Left;
        }

        public static implicit operator Window(DockPanel v)
        {
            throw new NotImplementedException();
        }
    }
}
