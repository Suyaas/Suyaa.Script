using Suyaa.Exceptions;
using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 当前作用域
    /// </summary>
    public sealed class SuCurrent : SuInstanceType
    {
        /// <summary>
        /// Il方法
        /// </summary>
        public IlMethod IlMethod { get; }

        /// <summary>
        /// 当前作用域
        /// </summary>
        public SuCurrent(IlMethod method) : base(new SuType(method.ClassType))
        {
            IlMethod = method;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IlField? GetField(string name)
        {
            return this.IlMethod.Fields.Where(d => d.Name == name).FirstOrDefault();
            //?? throw new SuException($"Field '{name}' not exists.");
        }

        #region 快速方法

        /// <summary>
        /// 创建一个方法
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public SuMethodInfo Method(SuStructType struc, string name)
        {
            return struc.GetMethod(name) ?? throw new SuException($"Method info '{struc.Name}.{name}' not found.");
        }

        /// <summary>
        /// 创建一个变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SuVariable Variable(string name)
        {
            return new SuVariable(name);
        }

        /// <summary>
        /// 创建一个外部类
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public SuType ExternClass(IlExternClass cls)
        {
            return new SuExternClass(cls);
        }

        /// <summary>
        /// 创建一个类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SuType Type(string name)
        {
            return new SuType(name);
        }

        /// <summary>
        /// 创建一个类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SuType Type(IlType type)
        {
            return new SuType(type);
        }

        /// <summary>
        /// 创建一个字段
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public SuField Field(IInstantiable obj, string name)
        {
            return new SuField(obj, name);
        }

        /// <summary>
        /// 创建一个结构体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public SuStructType Struct(ITypable obj)
        {
            switch (obj)
            {
                case SuField suField:
                    if (suField.Object is SuCurrent current)
                    {
                        var field = current[suField.Name];
                        return field.Type switch
                        {
                            IlString _ => new SuString(),
                            _ => new SuStructType(obj)
                        };
                    }
                    break;
            }
            return new SuStructType(obj);
        }

        /// <summary>
        /// 创建一个值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SuValue<T> Value<T>(T value)
            where T : notnull
        {
            return new SuValue<T>(value);
        }

        #endregion
    }
}
