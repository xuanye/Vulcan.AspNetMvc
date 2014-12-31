using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vulcan.AspNetMvc.Interfaces;

namespace SampleWebApp.Models.Service
{
    public class AppContextService : IAppContextService
    {

        public bool HasPrivilege(string identity, string privilegeCode)
        {
            return true; //实际情况 需要调用数据库或者外部服务等
        }

        public bool IsInRole(string identity, string roleCode)
        {
            return true;//实际情况 需要调用数据库或者外部服务等
        }

        public IUser GetUserInfo(string Identity)
        {
            //实际情况 需要调用数据库或者外部服务等
            return new IdentityUser()
            {
                UserId = "admin",
                FullName = "管理员",
                EmployID = "100001"
            };
        }
    }

    public class IdentityUser : IUser
    {

        public string UserId
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string EmployID
        {
            get;
            set;
        }

        public string GroupCode
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public string DeptCode
        {
            get;
            set;
        }

        public string DeptName
        {
            get;
            set;
        }

        public string OrgCode
        {
            get;
            set;
        }

        public string OrgName
        {
            get;
            set;
        }
    }
}