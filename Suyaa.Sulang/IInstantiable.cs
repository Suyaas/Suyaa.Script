using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 可实例化对象
    /// </summary>
    public interface IInstantiable : IStructable
    {
        /// <summary>
        /// 设置字段
        /// </summary>
        /// <param name="field"></param>
        void SetField(IlField field);

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="name"></param>
        IlField GetField(string name);
    }
}
