using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su常量
    /// </summary>
    public static class SuConsts
    {
        #region 全局对象
        // 全局对象
        private static SuGlobal? _global;
        /// <summary>
        /// 全局对象
        /// </summary>
        public static SuGlobal Global => _global ??= new SuGlobal();
        #endregion

        #region 无返回对象
        // 无返回对象
        private static SuVoid? _void;
        /// <summary>
        /// 无返回对象
        /// </summary>
        public static SuVoid Void => _void ??= new SuVoid();
        #endregion

        #region 整型
        // 整型
        private static SuInt32? _int32;
        /// <summary>
        /// 整型
        /// </summary>
        public static SuInt32 Int32 => _int32 ??= new SuInt32();
        #endregion

        #region 字符串
        // 字符串
        private static SuString? _str;
        /// <summary>
        /// 字符串
        /// </summary>
        public static SuString String => _str ??= new SuString();
        #endregion

    }
}
