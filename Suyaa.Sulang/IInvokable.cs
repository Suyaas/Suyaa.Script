using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 可执行对象
    /// </summary>
    public interface IInvokable : ISuable, ICodable
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Invoke();

        /// <summary>
        /// 获取执行结果返回类型
        /// </summary>
        /// <returns></returns>
        ITypable GetInvokeReutrnType();
    }
}
