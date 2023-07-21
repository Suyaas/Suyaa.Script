using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 带参数可执行对象
    /// </summary>
    public interface IParamInvokable : IInvokable
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="param"></param>
        void AddParam(ITypable param);
    }
}
