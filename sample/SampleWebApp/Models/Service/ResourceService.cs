using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleWebApp.Models.ServiceInterfaces;
using Vulcan.AspNetMvc.Interfaces;

namespace SampleWebApp.Models.Service
{
    public class ResourceService : IResourceService
    {
        private IDictService service;
        public ResourceService(IDictService service)
        {
            this.service = service;
        }
  
        public List<IResource> GetResourceByCode(string code)
        {
            return GetResourceByCode(code, false);
        }

        public List<IResource> GetResourceByCode(string code, bool hasAllOption)
        {
            return GetResourceByCode(code, "", false);
        }

        public List<IResource> GetResourceByCode(string code, string parentCode, bool hasAllOption)
        {
            List<IResource> list = null;
            switch (code)
            {

                case "GENDER":
                    list = new List<IResource>();
                    list.Add(new DefaultResource() { Code = "1", Name = "男", Value = "1" });
                    list.Add(new DefaultResource() { Code = "0", Name = "女", Value = "0" });
                    list.Add(new DefaultResource() { Code = "9", Name = "保密", Value = "9" });
                    break;
                // ... OTHER 

                // 数据字典 ，调用数据库或者其他什么
                case "DICTINFO": 

                    break;
            }
            if (list == null)
            {
                list = new List<IResource>();
            }
            return list;
        }
        
    }

    public class DefaultResource : IResource
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}