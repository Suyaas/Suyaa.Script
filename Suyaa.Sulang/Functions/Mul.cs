using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suyaa.Msil.Values;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Values;
using Suyaa.Exceptions;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 使用语句
    /// </summary>
    public sealed class Mul : SuMethodInfo
    {
        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="obj"></param>
        public Mul(ITypable obj) : base(obj, "Mul")
        {
            this.Declare(new IlType(nameof(IlType)));
            this.Return(obj);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method, SuParserCode code)
        {
            return new MulInvoker(method, code, this);
        }
    }

    /// <summary>
    /// Use方法执行器
    /// </summary>
    public class MulInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="code"></param>
        /// <param name="fun"></param>
        public MulInvoker(IlMethod method, SuParserCode code, Mul fun) : base(method, code, fun.Object, fun.Name)
        {
            this.Return(fun.Object);
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public override ITypable GetInvokeReutrnType()
        {
            return this.Object;
        }

        private void SetFieldString(SuFieldValue field, ITypable value)
        {
            // 添加对象
            IlMethod.Ldloc_s(new IlName(field.Value.Name));
            switch (value)
            {
                case SuValue<int> val:
                    IlMethod.Ldc_i4(val.Value);
                    break;
                case SuField suField:
                    IlMethod.Ldloc_s(new IlName(suField.Name));
                    break;
                default: throw new SuCodeException(Code, $"Not supported type '{value.GetType().FullName}'");
            }
            // 添加加法指令
            IlMethod.Mul();
            // 添加计算返回
            IlMethod.Stloc_s(new IlName(field.Value.Name));
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            if (this.Paramters.Count != 1) throw new SuException($"Method invoke does not match {nameof(Add)}(value)");
            var obj = this.Object;
            switch (obj)
            {
                // 处理字段
                case SuFieldValue suFieldValue:
                    SetFieldString(suFieldValue, this.Paramters[0]);
                    break;
                default: throw new NotSupportedException($"Method {nameof(Add)} not supported object '{obj.GetType()}'");
            }
        }
    }
}
