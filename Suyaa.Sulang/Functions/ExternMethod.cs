using Suyaa.Exceptions;
using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 外部函数
    /// </summary>
    public sealed class ExternMethod : SuMethodInfo
    {
        /// <summary>
        /// 外部函数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public ExternMethod(SuExternClass obj, string name) : base(obj, name)
        {
            var cls = (SuExternClass)this.Object;
            var clsType = sy.Assembly.FindType(cls.IlExternClass.Name);
            if (clsType is null) throw new SuException($"Extern class '{cls.IlExternClass.Name}' not found.");
            var methods = clsType.GetMethods().Where(d => d.Name == this.Name).ToList();
            if (!methods.Any()) throw new SuException($"Extern method '{cls.IlExternClass.Name}::{this.Name}' not found.");
            var type = methods.First().ReturnType;
            var typeCode = type.GetTypeCode();
            switch (typeCode)
            {
                case TypeCode.String: this.Return(Suable.String); break;
                case TypeCode.Object:
                    if (type.FullName == "System.Void")
                    {
                        this.Return(Suable.Void);
                        break;
                    }
                    throw new TypeNotSupportedException(type);
                default: throw new TypeNotSupportedException(type);
            }
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method)
        {
            var invoker = new ExternMethodInvoker(method, this);
            invoker.Declares.AddRange(this.Declares);
            invoker.Keywords.AddRange(this.Keywords);
            invoker.Return(this.ReturnType);
            return invoker;
        }
    }

    /// <summary>
    /// 外部函数执行器
    /// </summary>
    public sealed class ExternMethodInvoker : SuMethodInvoker
    {
        /// <summary>
        /// 外部函数执行器
        /// </summary>
        /// <param name="method"></param>
        /// <param name="externMethod"></param>
        public ExternMethodInvoker(IlMethod method, ExternMethod externMethod) : base(method, externMethod.Object, externMethod.Name)
        {
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public override ITypable GetInvokeReutrnType()
        {
            return this.ReturnType;
        }
    }
}
