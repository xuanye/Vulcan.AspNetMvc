using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulcan.AspNetMvc.Request
{
    /// <summary>
    /// 自定义字符串比较 ，使用AsciI码比大小
    /// </summary>
    public class StringCompare : IComparer<string>
    {
        #region IComparer<string> Members

        /// <summary>
        /// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
        /// </summary>
        /// <param name="x">要比较的第一个对象。</param>
        /// <param name="y">要比较的第二个对象。</param>
        /// <returns>
        /// 值 条件 小于零<paramref name="x"/> 小于 <paramref name="y"/>。零<paramref name="x"/> 等于 <paramref name="y"/>。大于零<paramref name="x"/> 大于 <paramref name="y"/>。
        /// </returns>
        public int Compare(string x, string y)
        {
            return string.CompareOrdinal(x, y);
        }

        #endregion
    }
}
