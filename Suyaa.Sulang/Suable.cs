using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// Su可处理对象
    /// </summary>
    public abstract class Suable : Disposable, ISuable
    {
        /// <summary>
        /// 获取当前工作对象
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static SuCurrent Current(IlMethod method)
        {
            return new SuCurrent(method);
        }

        #region 全局对象
        // 全局对象
        private static SuGlobal? _global;
        /// <summary>
        /// 全局对象
        /// </summary>
        public static SuGlobal Global => _global ??= new SuGlobal();
        #endregion

        #region 堆栈对象
        // 堆栈对象
        private static SuStack? _stack;
        /// <summary>
        /// 堆栈对象
        /// </summary>
        public static SuStack Stack => _stack ??= new SuStack();
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
