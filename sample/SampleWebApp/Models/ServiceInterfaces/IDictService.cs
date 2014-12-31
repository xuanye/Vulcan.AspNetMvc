using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebApp.Models.ServiceInterfaces
{
    public interface IDictService
    {
        DictInfo QueryDictListByParentCode(string parentCode);
    }
}