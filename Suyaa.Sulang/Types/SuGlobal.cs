using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suyaa.Sulang.Functions;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 全局对象$类型
    /// </summary>
    public sealed class IlGlobal : IlType
    {
        /// <summary>
        /// 全局对象$类型
        /// </summary>
        public IlGlobal() : base("$")
        {
        }
    }

    /// <summary>
    /// 全局对象$
    /// </summary>
    public sealed class SuGlobal : SuInstanceType, IInstantiable
    {
        // 类型
        private static SuType _type = new SuType(new IlGlobal());

        /// <summary>
        /// 全局对象$
        /// </summary>
        public SuGlobal() : base(_type)
        {
            // Use函数
            this.Methods.Add(new Use(this));
            // Step函数
            this.Methods.Add(new Step(this));
            // Int函数
            this.Methods.Add(new Functions.Int32(this));
            // String函数
            this.Methods.Add(new Functions.String(this));
            // Join函数
            this.Methods.Add(new Functions.Join(this));
        }
    }
}
