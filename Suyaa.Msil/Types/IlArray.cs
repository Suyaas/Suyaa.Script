using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// 数组
    /// </summary>
    public class IlArray : IlType
    {
        /// <summary>
        /// 包含类型
        /// </summary>
        public IlType Type { get; }

        /// <summary>
        /// 数组
        /// </summary>
        public IlArray(IlType type) : base("array")
        {
            Type = type;
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return this.Type.ToAssembly() + "[]";
        }
    }

    /// <summary>
    /// 数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IlArray<T> : IlArray
        where T : IlType
    {
        public IlArray() : base(sy.Assembly.Create<T>())
        {
        }
    }
}
