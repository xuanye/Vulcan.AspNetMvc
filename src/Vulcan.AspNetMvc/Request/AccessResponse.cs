using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Vulcan.AspNetMvc.Request
{
    [DataContract]
    public class AccessResponse
    {
        [DataMember(Name="status")]
        public int Status
        {
            get;
            set;
        }
        [DataMember(Name = "msg")]
        public string Message
        {
            get;
            set;
        }
  
    }
    [DataContract]
    public class AccessResponse<T> : AccessResponse
    {
        [DataMember(Name = "data")]
        public T Data
        { get; set; }
    }
}
