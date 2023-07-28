using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// Su可处理对象
    /// </summary>
    public abstract class Suable : Disposable, ISuable
    {
        /// <summary>
        /// 获取当前工作对象
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static SuCurrent Current(IlMethod method)
        {
            return new SuCurrent(method);
        }
    }
}
