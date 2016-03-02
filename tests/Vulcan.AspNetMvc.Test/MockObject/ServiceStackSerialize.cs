using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vulcan.AspNetMvc.Interfaces;
using ServiceStack.Text;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public class ServiceStackSerialize : IJsonSerialize
    {
        public T FromJson<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            else
            {
                return str.FromJson<T>();
            }
        }

        public string ToJson(object value)
        {
           if(value == null)
           {
               return ""; 
           }
           else
           {
               return value.ToJson();
           }
        }
    }
}
