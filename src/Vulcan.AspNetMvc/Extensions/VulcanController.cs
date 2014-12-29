using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Vulcan.AspNetMvc.Interfaces;

namespace Vulcan.AspNetMvc.Extensions
{
    public class VulcanController:Controller
    {
        /// <summary>
        /// 获取当前登录用户标识
        /// </summary>
        /// <value>The user ID.</value>
        protected virtual string UserId
        {
            get
            {
                return AppContext.Identity;
            }
        }

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        /// <value>The current user.</value>
        protected virtual IUser CurrentUser
        {
            get
            {
                return AppContext.CurrentUser;
            }
        }

        /// <summary>
        /// 判断用户是否拥有某个权限
        /// </summary>
        /// <param name="privilegeCode">The privilege code.</param>
        /// <returns>
        ///   <c>true</c> 是否拥有指定权限<c>false</c>.
        /// </returns>
        protected virtual bool HasPrivilege(string privilegeCode)
        {
            return AppContext.HasPrivilege(this.UserId, privilegeCode);
        }

        /// <summary>
        /// 判断用户是否属于某个角色
        /// </summary>
        /// <param name="roleCode">角色标识</param>
        /// <returns>
        /// 	<c>true</c> 是否属于某个角色 <c>false</c>.
        /// </returns>
        protected virtual bool IsInRole(string roleCode)
        {
            return AppContext.IsInRole(this.UserId,roleCode);
        }


        //重写基类的方法
        #region 重写基类的方法
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ServiceStackJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
        #endregion
    }
}
