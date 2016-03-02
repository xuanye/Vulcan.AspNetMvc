using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Vulcan.AspNetMvc.Extensions
{
    public static class HtmlHelperExtension
    {      
        public static MvcHtmlString Css(this HtmlHelper html, params string[] cssfilename)
        {
            if (cssfilename != null)
            {
                string folderpath = "~/static/css";
                string csslink = "<link href=\"{0}\" rel=\"Stylesheet\" type=\"text/css\" />";
                StringBuilder sb = new StringBuilder();
              
                foreach (string filename in cssfilename)
                {
                     sb.AppendFormat(csslink, UrlHelper.GenerateContentUrl(folderpath + "/" + filename + ".css", html.ViewContext.HttpContext));
                }

                return MvcHtmlString.Create(sb.ToString());
            }
            return MvcHtmlString.Empty;

        }
    }
}
