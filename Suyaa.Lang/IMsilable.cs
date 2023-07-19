using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang
{
    /// <summary>
    /// 可Msil化对象
    /// </summary>
    public interface IMsilable
    {
        /// <summary>
        /// 转化为可编译对象
        /// </summary>
        /// <returns></returns>
        IAssemblable ToAssemblable();
    }
}
