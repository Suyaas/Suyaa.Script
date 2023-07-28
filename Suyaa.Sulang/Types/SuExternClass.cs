using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// Su外部类
    /// </summary>
    public class SuExternClass : SuStructType
    {

        /// <summary>
        /// Il外部类
        /// </summary>
        public IlExternClass IlExternClass { get; }

        /// <summary>
        /// IL外部类
        /// </summary>
        /// <param name="cls"></param>
        public SuExternClass(IlExternClass cls) : base(cls)
        {
            IlExternClass = cls;
        }

        /// <summary>
        /// 获取方法信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override SuMethodInfo? GetMethod(string name)
        {
            return new SuMethodInfo(this, name).Keyword(IlKeys.Static);
        }
    }
}
