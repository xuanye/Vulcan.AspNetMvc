using System;
using System.Collections.Generic;
using System.Text;

namespace Vulcan.AspNetMvc.Request
{
    public class ResponseEventArgs : EventArgs
    {
        public ResponseEventArgs(string response)
        {
            this.Response = response;
        }
        public string Response { get; set; }
    }
}
