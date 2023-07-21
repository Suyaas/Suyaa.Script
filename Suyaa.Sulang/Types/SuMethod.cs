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
    public class SuMethod : NamedSuable, IParamInvokable
    {
        /// <summary>
        /// 所属结构体
        /// </summary>
        public SuStruct Struct { get; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public IlType? ReturnType { get; private set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<ITypable> Paramters { get; }

        #region 快捷函数

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public SuMethod Param(params ITypable[] paramters)
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
        /// <param name="name"></param>
        public SuMethod(SuStruct struc, string name) : base(name)
        {
            Struct = struc;
            Paramters = new List<ITypable>();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="method"></param>
        public virtual void Invoke(IlMethod method)
        {
            // 建立il执行器
            var invoker = new IlMethodInvoker(this.Struct.GetIlType(), this.Name);
            // 判断是否为静态调用
            var field = this.Struct.Object?.GetField(this.Struct.Name);
            invoker.IsStatic = field?.Keywords.Contains(SuKeys.Class) ?? false;
            // 添加返回类型
            if (this.ReturnType != null) invoker.Return(this.ReturnType);
            // 处理参数
            foreach (var p in Paramters)
            {
                // 添加参数类型
                invoker.Param(p.GetIlType());
                // 参数内容处理
                switch (p)
                {
                    // 字符串对象
                    case SuStringValue str:
                        method.Ldstr(new IlStringValue(str.Value));
                        break;
                }
            }
            // 执行
            method.Call(invoker);
        }
    }
}
