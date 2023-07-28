using Suyaa.Msil;
using Suyaa.Msil.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su值
    /// </summary>
    public static class IlConsts
    {
        #region 空
        // 空
        private static IlNull? _null;
        /// <summary>
        /// 空
        /// </summary>
        public static IlNull Null => _null ??= new IlNull();
        #endregion

        #region 变量
        // 变量
        private static IlVariable? _variable;
        /// <summary>
        /// 变量
        /// </summary>
        public static IlVariable Variable => _variable ??= new IlVariable(string.Empty);
        #endregion

        #region 整型
        // 整型
        private static IlInt32? _int32;
        /// <summary>
        /// 整型
        /// </summary>
        public static IlInt32 Int32 => _int32 ??= new IlInt32();
        #endregion

        #region 字符串
        // 字符串
        private static IlString? _str;
        /// <summary>
        /// 字符串
        /// </summary>
        public static IlString String => _str ??= new IlString();
        #endregion

    }
}
