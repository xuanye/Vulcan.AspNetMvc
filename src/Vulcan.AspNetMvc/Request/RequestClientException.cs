using System;
using System.Collections.Generic;
using System.Text;

namespace Vulcan.AspNetMvc.Request
{
    public class RequestClientException:Exception
    {
        public RequestClientException(string code)
        {
            this.ErrorCode = code;
        }
        public RequestClientException(string code,string msg)
            : base(msg)
        {
            this.ErrorCode = code;
        }
        public RequestClientException(string code, string msg, Exception inner)
            : base(msg,inner)
        {
            this.ErrorCode = code;
        }

        public string ErrorCode
        {
            get;
            set;
        }
    }

    public class RequestClientErrorCode
    { 
       public const string NoAuth = "NoAuth";
       public const string ConfigError = "ConfigError";
       public const string NoCategory = "NoCategory";
    }
}
