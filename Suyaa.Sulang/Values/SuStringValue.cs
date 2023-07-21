using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Values
{
    /// <summary>
    /// Su字符串
    /// </summary>
    public class SuStringValue : SuValue<string>, ITypable
    {
        // Su字符串
        public SuStringValue(string value) : base(value)
        {
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            return new IlString();
        }
    }
}
