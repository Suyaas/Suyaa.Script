using Suyaa.Msil;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 可结构化对象
    /// </summary>
    public interface IStructable : ITypable
    {
        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SuMethodInfo? GetMethod(string name);
    }
}
