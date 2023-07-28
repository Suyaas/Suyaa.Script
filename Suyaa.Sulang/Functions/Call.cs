using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 调用函数
    /// </summary>
    public sealed class Call : Suable, IInvokable
    {

        /// <summary>
        /// 所属对象
        /// </summary>
        public IParamInvokable Invoker { get; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public List<ITypable> Paramters { get; }

        /// <summary>
        /// 调用函数
        /// </summary>
        /// <param name="invoker">执行对象</param>
        public Call(IParamInvokable invoker)
        {
            Invoker = invoker;
            this.Paramters = new List<ITypable>();
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Call Param(ITypable param)
        {
            this.Paramters.Add(param);
            return this;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param"></param>
        public void AddParam(ITypable param)
        {
            this.Paramters.Add(param);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Invoke()
        {
            foreach (var param in this.Paramters) this.Invoker.AddParam(param);
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public ITypable GetInvokeReutrnType()
        {
            return SuConsts.Void;
        }

        /// <summary>
        /// 获取代码
        /// </summary>
        /// <returns></returns>
        public string ToCodeString()
        {
            return this.Invoker.ToCodeString();
        }
    }
}
