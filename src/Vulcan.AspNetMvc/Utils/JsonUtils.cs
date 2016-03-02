using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vulcan.AspNetMvc.Common;
using Vulcan.AspNetMvc.Interfaces;


namespace Vulcan.AspNetMvc.Utils
{
    public static class JsonUtils
    {
        private static IJsonSerialize serialize = ServiceFactory.GetInstance<IJsonSerialize>();
        public static T FromJson<T>(string str)
        {
            if (str == null) str = string.Empty;

            return serialize.FromJson<T>(str);
            
        }

        public static string ToJson(Object value)
        {
            if (value == null) return string.Empty;
          
            return serialize.ToJson(value);
        }
    }
}
