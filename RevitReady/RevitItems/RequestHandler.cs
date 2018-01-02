using System;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CefSharp.MinimalExample.Wpf.RevitItems
{
    public class RequestHandler : IExternalEventHandler
    {
        private Request m_request = new Request();

        public Request Request
        {
            get { return m_request; }
        }

        public String GetName()
        {
            return "Revit Ready Video Library";
        }

        public void Execute(UIApplication uiapp)
        {
            try
            {
                switch (Request.Take())
                {
                    case RequestId.None:
                        {
                            return;  // no request at this time -> we can leave immediately
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            finally
            {

            }
            return;
        }
    }
}
