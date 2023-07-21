using Suyaa.Msil;
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
        /// 创建一个方法
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SuMethodInfo Method(SuStruct struc, string name)
        {
            return new SuMethodInfo(struc, name);
        }

        /// <summary>
        /// 创建一个变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SuVariable Variable(string name)
        {
            return new SuVariable(name);
        }

        /// <summary>
        /// 创建一个类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SuType Type(string name)
        {
            return new SuType(name);
        }

        /// <summary>
        /// 创建一个类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SuType Type(IlType type)
        {
            return new SuType(type);
        }

        /// <summary>
        /// 创建一个结构体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SuStruct Struct(IInstantiable? obj, string name)
        {
            return new SuStruct(obj, name);
        }

        /// <summary>
        /// 创建一个值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SuValue<T> Value<T>(T value)
            where T : notnull
        {
            return new SuValue<T>(value);
        }
    }
}
