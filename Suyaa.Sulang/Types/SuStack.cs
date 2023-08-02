using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 堆栈类型
    /// </summary>
    public sealed class IlStack : IlType
    {
        /// <summary>
        /// 堆栈类型
        /// </summary>
        public IlStack() : base("IlStack")
        {
        }
    }

    /// <summary>
    /// 堆栈对象
    /// </summary>
    public sealed class SuStack : SuType
    {
        // 类型
        private static SuType _type = new SuType(new IlStack());

        /// <summary>
        /// 堆栈对象
        /// </summary>
        public SuStack() : base(_type.GetIlType())
        {
        }

    }
}
