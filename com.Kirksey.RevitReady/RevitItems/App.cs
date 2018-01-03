using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Windows;

using com.Kirksey.RevitReady.Interface;

namespace com.Kirksey.RevitReady.RevitItems
{
    class App : IExternalApplication
    {
        public static App thisApp;
        private UIControlledApplication _uiApp;
        public static UIApplication uiApp;

        protected internal static DockPanel _dockPanel;
        protected internal static DockablePaneId paneId;

        private string _path;
        private string _assembly;

        public Result OnStartup(UIControlledApplication a)
        {
            thisApp = this;
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _dockPanel = null;

            _assembly = Assembly.GetExecutingAssembly().GetName().Name;

            if (_dockPanel == null)
            {
                RequestHandler handler = new RequestHandler();
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                _dockPanel = new DockPanel(exEvent, handler);
            }

            paneId = new DockablePaneId(Guid.NewGuid());
            a.RegisterDockablePane(paneId, "Videos", (IDockablePaneProvider)_dockPanel);

            //Load Ribbon and stuff.

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        private void RibbonTabStuff()
        {
            string _revitReady = Path.Combine(_path, _assembly);
            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            var tab = ribbon.FindTab("Kirksey");

            if (tab == null)
            {
                //create a tab
                _uiApp.CreateRibbonTab("Kirksey");
            }

            if (File.Exists(_revitReady))
            {
                Autodesk.Revit.UI.RibbonPanel _rRRibbonPanel = GetRibbonPanelByTabName("Kirksey", "Revit Ready");
                AddButton(_rRRibbonPanel, "Revit Ready", "Show/Hide \nVideo Browser", _revitReady, "RevitReady.Command", "RevitReady.Images.RevitReady16.png", "RevitReady.Images.RevitReady32.png", "Show and Hide the Revit Ready Video Browser", "");
            }
        }

        private Autodesk.Revit.UI.RibbonPanel GetRibbonPanelByTabName(string tabName, string panelName)
        {
            Autodesk.Revit.UI.RibbonPanel m_panel = null;
            bool exists = false;
            try
            {
                // Does it exist already?
                foreach (Autodesk.Revit.UI.RibbonPanel x in _uiApp.GetRibbonPanels(tabName))
                {
                    if (x.Title.ToLower() == panelName.ToLower())
                    {
                        exists = true;
                    }
                }
            }
            catch
            {
            }
            if (exists == false)
            {
                try
                {
                    // Add the Panel
                    m_panel = _uiApp.CreateRibbonPanel(tabName, panelName);
                }
                catch
                {
                    m_panel = null;
                }
            }
            return m_panel;
        }

        private void AddButton(Autodesk.Revit.UI.RibbonPanel rpanel, string buttonName, string buttonText, string dllPath, string dllClass, string imagePath16, string imagePath32, string toolTip, string pbAvail)
        {
            PushButtonData m_pbData = GetPushButtonData(buttonName, buttonText, dllPath, dllClass, imagePath16, imagePath32, toolTip, pbAvail);
            rpanel.AddItem(m_pbData);
        }

        private PushButtonData GetPushButtonData(string cmdName, string cmdText, string filePath, string className, string img16, string img32, string tooltipText, string cmdAvail)
        {
            PushButtonData m_pb = new PushButtonData(cmdName, cmdText, filePath, className);
            m_pb.Image = LoadPngImgSource(img16);
            m_pb.LargeImage = LoadPngImgSource(img32);
            m_pb.ToolTip = tooltipText;
            return m_pb;
        }

        private ImageSource LoadPngImgSource(string sourceName)
        {
            Assembly m_assembly = Assembly.GetExecutingAssembly();
            Stream m_icon = m_assembly.GetManifestResourceStream(sourceName);
            PngBitmapDecoder m_decoder = new PngBitmapDecoder(m_icon, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            ImageSource m_source = m_decoder.Frames[0];
            return m_source;
        }
    }
}
