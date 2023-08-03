using Suyaa.Msil;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 申明一个字符串变量
    /// </summary>
    public sealed class String : SuMethodInfo
    {

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public IlType Type { get; }

        /// <summary>
        /// 申明一个字符串变量
        /// </summary>
        /// <param name="sg"></param>
        public String(SuGlobal sg) : base(sg, "String")
        {
            //Type = type;
            this.Declare(new IlType(nameof(IlType)));
            this.Return(Suable.String);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method, SuParserCode code)
        {
            return new StringInvoker(method, code, this);
        }
    }


    /// <summary>
    /// Int方法执行器
    /// </summary>
    public class StringInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="code"></param>
        /// <param name="fn"></param>
        public StringInvoker(IlMethod method, SuParserCode code, String fn) : base(method, code, fn.Object, fn.Name)
        {
            // 设置为预执行
            this.IsPreInvoke = true;
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SuException"></exception>
        public override ITypable GetInvokeReutrnType()
        {
            if (this.Paramters.Count != 1) throw new SuException("Method invoke does not match String(var)");
            if (!(this.Paramters[0] is SuVariable variable)) throw new SuException("Method invoke does not match String(var)");
            // 字段
            var field = new SuField(Current(this.IlMethod), variable.VarName);
            // 返回包含字段和类型的值对象
            return new SuFieldValue(field, Suable.String);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            if (this.Paramters.Count != 1) throw new SuException("Method invoke does not match String(var)");
            if (!(this.Paramters[0] is SuVariable variable)) throw new SuException("Method invoke does not match String(var)");
            IlMethod.Field(variable.VarName, IlConsts.String);
        }
    }
}
