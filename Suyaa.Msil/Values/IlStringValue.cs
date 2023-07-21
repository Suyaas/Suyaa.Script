using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Values
{
    /// <summary>
    /// Il字符串值
    /// </summary>
    public sealed class IlStringValue : IlValue<string>
    {
        /// <summary>
        /// Il字符串值
        /// </summary>
        /// <param name="value"></param>
        public IlStringValue(string value) : base(value)
        {
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $"\"{this.Value}\"";
        }
    }
}
