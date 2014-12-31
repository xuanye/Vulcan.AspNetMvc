using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleWebApp.Models.ServiceInterfaces;

namespace SampleWebApp.Models.Service
{
    public class DictService : IDictService
    {
        // 实现访问数据库的 数字字典
        public DictInfo QueryDictListByParentCode(string parentCode)
        {
            throw new NotImplementedException("没有实现");
        }
    }
}