using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 无返回
    /// </summary>
    public sealed class SuVoid : Suable, ITypable
    {
        // 类型
        private static IlType? _type;

        /// <summary>
        /// 静态类型
        /// </summary>
        public new static IlType Type { get => _type ??= new IlType(nameof(SuVoid)); }

        /// <summary>
        /// 获取Il类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            return Type;
        }
    }
}
