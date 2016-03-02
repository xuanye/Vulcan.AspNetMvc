using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vulcan.AspNetMvc.Interfaces;
using Newtonsoft.Json;

namespace Vulcan.AspNetMvc.DefaultImpl
{
    public class DefaultJsonSerialize: IJsonSerialize
    {
        public T FromJson<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
