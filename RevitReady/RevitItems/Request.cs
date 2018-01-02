using System.Threading;

namespace CefSharp.MinimalExample.Wpf.RevitItems
{
    public enum RequestId : int
    {
        None = 0
    }


    public class Request
    {
        private int m_request = (int)RequestId.None;

        public RequestId Take()
        {
            return (RequestId)Interlocked.Exchange(ref m_request, (int)RequestId.None);
        }

        public void Make(RequestId request)
        {
            Interlocked.Exchange(ref m_request, (int)request);
        }
    }
}
