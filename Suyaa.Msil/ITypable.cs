using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 可转为类型的对象
    /// </summary>
    public interface ITypable
    {
        /// <summary>
        /// 获取Il类型
        /// </summary>
        /// <returns></returns>
        IlType GetIlType();
    }
}
