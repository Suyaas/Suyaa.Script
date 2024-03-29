﻿using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 无返回
    /// </summary>
    public sealed class SuVoid : SuStructType, ITypable
    {
        // 类型
        private static SuType _type = new SuType(new IlType("void"));

        /// <summary>
        /// 无返回
        /// </summary>
        public SuVoid() : base(_type)
        {
        }
    }
}
