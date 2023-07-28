using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 可代码化
    /// </summary>
    public interface ICodable
    {
        /// <summary>
        /// 获取代码字符串
        /// </summary>
        /// <returns></returns>
        string ToCodeString();
    }
}
