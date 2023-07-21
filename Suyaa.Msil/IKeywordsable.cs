using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 带关键字描述对象
    /// </summary>
    public interface IKeywordsable
    {
        /// <summary>
        /// 关键字
        /// </summary>
        List<string> Keywords { get; }
    }
}
