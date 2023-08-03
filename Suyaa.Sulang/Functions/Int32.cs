using Suyaa.Msil;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 申明一个整型变量
    /// </summary>
    public sealed class Int32 : SuMethodInfo
    {
        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="sg"></param>
        public Int32(SuGlobal sg) : base(sg, "Int32")
        {
            this.Declare(new IlType(nameof(IlType)));
            this.Return(Suable.Int32);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method, SuParserCode code)
        {
            return new SuIntInvoker(method, code, this);
        }
    }


    /// <summary>
    /// Int方法执行器
    /// </summary>
    public class SuIntInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="code"></param>
        /// <param name="fun"></param>
        public SuIntInvoker(IlMethod method, SuParserCode code, Int32 fun) : base(method, code, fun.Object, fun.Name)
        {
            // 设置为预执行
            this.IsPreInvoke = true;
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public override ITypable GetInvokeReutrnType()
        {
            if (this.Paramters.Count != 1) throw new SuException($"Method invoke does not match {nameof(Int32)}(var)");
            if (!(this.Paramters[0] is SuVariable variable)) throw new SuException($"Method invoke does not match {nameof(Int32)}(var)");
            // 字段
            var field = new SuField(Current(this.IlMethod), variable.VarName);
            // 返回包含字段和类型的值对象
            return new SuFieldValue(field, Suable.Int32);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            if (this.Paramters.Count != 1) throw new SuException($"Method invoke does not match {nameof(Int32)}(var)");
            if (!(this.Paramters[0] is SuVariable variable)) throw new SuException($"Method invoke does not match {nameof(Int32)}(var)");
            IlMethod.Field(variable.VarName, IlConsts.Int32);
        }
    }
}
