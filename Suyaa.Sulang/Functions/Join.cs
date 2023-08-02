using Suyaa.Exceptions;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
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
    public sealed class Join : SuMethodInfo
    {
        ///// <summary>
        ///// 类型
        ///// </summary>
        //public IlType Type { get; }

        /// <summary>
        /// 申明一个字符串变量
        /// </summary>
        /// <param name="sg"></param>
        public Join(SuGlobal sg) : base(sg, "Join")
        {
            //Type = type;
            this.Declare(new IlType(nameof(IlType)));
            this.Return(Suable.String);
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method)
        {
            return new JoinInvoker(method, this);
        }
    }


    /// <summary>
    /// Int方法执行器
    /// </summary>
    public class JoinInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="fn"></param>
        public JoinInvoker(IlMethod method, Join fn) : base(method, fn.Object, fn.Name)
        {
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SuException"></exception>
        public override ITypable GetInvokeReutrnType()
        {
            // 返回堆栈值对象
            return new SuStackValue(Suable.String);
        }

        // 执行参数
        private void InvokeParamter(ITypable obj)
        {
            switch (obj)
            {
                case SuValue<string> str:
                    IlMethod.Ldstr(new IlValue<string>(str.Value));
                    break;
                case SuField suField:
                    IlMethod.Ldloc_s(new IlName(suField.Name));
                    break;
                default: throw new TypeNotSupportedException(obj.GetType());
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            if (this.Paramters.Count < 2) throw new SuException($"Method '{nameof(Join)}' invoke minimum 2 parameters.");
            // 初始化Concat函数
            var msCorlib = new MsCorlib();
            var invoker = new IlMethodInvoker(msCorlib.GetIlExternClass("System.String"), "Concat") { IsStatic = true };
            invoker.Return(IlConsts.String);
            foreach (var p in this.Paramters)
            {
                // 处理参数
                InvokeParamter(p);
                // 添加Concat函数参数
                invoker.Param<IlString>();
            }
            // 添加Concat函数调用
            IlMethod.Call(invoker);
        }
    }
}
