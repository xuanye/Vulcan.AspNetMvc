using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.AspNetMvc.Request
{
    public class ClientErrorEventArgs:EventArgs
    {
        public ClientErrorEventArgs(string msg, RequestClientException ex)
        {
            this.ErrorMsg = msg;
            this.Exception = ex;
        }

        public string ErrorMsg { get;set; }

        public RequestClientException Exception { get; set; }
        
    }
}
