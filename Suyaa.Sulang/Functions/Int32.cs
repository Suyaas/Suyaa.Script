using Suyaa.Msil;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
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
        /// 所属对象
        /// </summary>
        public SuGlobal Global { get; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public IlType Type { get; }

        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="sg"></param>
        public Int32(SuGlobal sg) : base(sg, "Use")
        {
            Global = sg;
            //Type = type;
            this.Declare(new IlType(nameof(IlType)));
            this.Return(SuConsts.Int32);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method)
        {
            return new SuIntInvoker(method, this);
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
        /// <param name="suUse"></param>
        public SuIntInvoker(IlMethod method, Int32 suUse) : base(method, suUse.Global, suUse.Name)
        {
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public override ITypable GetInvokeReutrnType()
        {
            return new SuField(Current(this.IlMethod), this.Name);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            IlMethod.Field(this.Name, IlConsts.Int32);
        }
    }
}
