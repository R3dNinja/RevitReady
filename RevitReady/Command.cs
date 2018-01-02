using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CefSharp.MinimalExample.Wpf
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        internal protected static Document m_document = null;
        internal protected UIDocument m_documentUI;
        internal protected static UIApplication m_uiapp;
        internal protected static string notePath;
        //applications's private data
        private AddInId m_thisAppId;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            m_document = doc;
            m_uiapp = uiapp;
            m_thisAppId = uiapp.Application.ActiveAddInId;

            DockablePane pane = uiapp.GetDockablePane(RevitApp.paneId);
            if (pane != null)
            {
                if (!pane.IsShown())
                    pane.Show();
                else
                    pane.Hide();
                
            }

            return Result.Succeeded;
        }
    }
}
