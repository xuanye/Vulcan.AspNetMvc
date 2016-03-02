using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.AspNetMvc.Interfaces
{
    public interface IJsonSerialize
    {
        T FromJson<T>(string str);

        string ToJson(Object value);
    }
}
