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

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 使用语句
    /// </summary>
    public sealed class Set : SuMethodInfo
    {
        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="obj"></param>
        public Set(ITypable obj) : base(obj, "Set")
        {
            this.Declare(new IlType(nameof(IlType)));
            this.Return(obj);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method)
        {
            return new SetInvoker(method, this);
        }
    }

    /// <summary>
    /// Use方法执行器
    /// </summary>
    public class SetInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="fun"></param>
        public SetInvoker(IlMethod method, Set fun) : base(method, fun.Object, fun.Name)
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
            switch (value)
            {
                case SuValue<string> str:
                    IlMethod.Ldstr(new IlValue<string>(str.Value));
                    IlMethod.Stloc_s(new IlName(field.Value.Name));
                    break;
                case SuStack _:
                    IlMethod.Stloc_s(new IlName(field.Value.Name));
                    break;
                default: throw new SuException("Method invoke does not match Set(value)");
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            if (this.Paramters.Count != 1) throw new SuException($"Method invoke does not match Set(value)");
            var obj = this.Object;
            switch (obj)
            {
                // 处理字段
                case SuFieldValue suFieldValue:
                    SetFieldString(suFieldValue, this.Paramters[0]);
                    break;
                default: throw new NotSupportedException($"Method Set not supported object '{obj.GetType()}'");
            }
        }
    }
}
