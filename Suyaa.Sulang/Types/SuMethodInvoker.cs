using Suyaa.Sulang.Values;
using Suyaa.Msil;
using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su方法
    /// </summary>
    public class SuMethodInvoker : SuMethodInfo, IParamInvokable, IKeywordsable
    {
        /// <summary>
        /// 参数
        /// </summary>
        public List<ITypable> Paramters { get; }

        #region 快捷函数

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public SuMethodInvoker Param(params ITypable[] paramters)
        {
            Paramters.AddRange(paramters.ToList());
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

        #endregion

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="name"></param>
        public SuMethodInvoker(SuStruct struc, string name) : base(struc, name)
        {
            this.Paramters = new List<ITypable>();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="method"></param>
        public virtual void Invoke(IlMethod method)
        {
            // 处理参数
            foreach (var p in Paramters)
            {
                // 参数内容处理
                switch (p)
                {
                    // 字符串对象
                    case SuValue<string> str:
                        method.Ldstr(new IlStringValue(str.Value));
                        break;
                }
            }
            // 建立il执行器
            var invoker = new IlMethodInvoker(this.Object.GetIlType(), this.Name);
            // 判断是否为静态调用
            if (this.Keywords.Contains(IlKeys.Static))
            {
                invoker.IsStatic = true;
            }
            else
            {
                var field = this.Object.Object?.GetField(this.Object.Name);
                invoker.IsStatic = field?.Keywords.Contains(SuKeys.Class) ?? false;
            }
            // 添加返回类型
            if (this.ReturnType != null) invoker.Return(this.ReturnType);
            // 处理参数定义
            foreach (var type in Declares)
            {
                // 添加参数类型
                invoker.Param(type);
            }
            // 执行
            method.Call(invoker);
        }
    }
}
