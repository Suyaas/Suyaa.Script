using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 可汇编对象
    /// </summary>
    public interface IAssemblable : IDisposable
    {
        /// <summary>
        /// 转化为汇编字符串
        /// </summary>
        /// <returns></returns>
        string ToAssembly();
    }
}
