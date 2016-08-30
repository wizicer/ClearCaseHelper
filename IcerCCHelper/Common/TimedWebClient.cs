using System;
using System.Net;

namespace IcerDesign.CCHelper
{
    public class TimedWebClient : WebClient
    {
        public TimedWebClient()
        {
            this.Timeout = 600000;
        }

        // Timeout in milliseconds, default = 600,000 msec
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }
}