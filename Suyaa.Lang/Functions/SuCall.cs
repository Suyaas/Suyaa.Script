using Suyaa.Lang.Exceptions;
using Suyaa.Lang.Types;
using Suyaa.Lang.Values;
using Suyaa.Msil;
using Suyaa.Msil.Consts;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Lang.Functions
{
    /// <summary>
    /// 调用函数
    /// </summary>
    public class SuCall : Disposable, IInvokable
    {

        /// <summary>
        /// 所属对象
        /// </summary>
        public IInstantiable Object { get; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 执行器
        /// </summary>
        public IlMethodInvoker Invoker { get; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public List<ISuable> Paramters { get; }

        /// <summary>
        /// 调用函数
        /// </summary>
        /// <param name="method">所属方法</param>
        /// <param name="invoker">执行对象</param>
        public SuCall(IInstantiable obj, string name, IlMethodInvoker invoker)
        {
            Object = obj;
            Name = name;
            Invoker = invoker;
            this.Paramters = new List<ISuable>();
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public SuCall Param(ISuable param)
        {
            this.Paramters.Add(param);
            return this;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Invoke()
        {
            // 处理参数
            foreach (var p in Paramters)
            {
                switch (p)
                {
                    // 字符串对象
                    case SuStringValue str:
                        this.Method.Ldstr(new IlStringValue(str.Value));
                        break;
                }
            }
            // 执行
            this.Method.Call(this.Invoker);
        }
    }
}
