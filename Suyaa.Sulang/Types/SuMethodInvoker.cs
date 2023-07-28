using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su方法
    /// </summary>
    public class SuMethodInvoker : SuMethodInfo, IParamInvokable, IKeywordsable, ICodable
    {

        /// <summary>
        /// 参数
        /// </summary>
        public List<ITypable> Paramters { get; }

        /// <summary>
        /// 所属Il方法
        /// </summary>
        public IlMethod IlMethod { get; }

        /// <summary>
        /// 是否提前执行
        /// </summary>
        public bool IsPreInvoke { get; protected set; }

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
        /// <param name="method"></param>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public SuMethodInvoker(IlMethod method, ITypable obj, string name) : base(obj, name)
        {
            this.Paramters = new List<ITypable>();
            IlMethod = method;
        }

        /// <summary>
        /// 推导执行返回
        /// </summary>
        /// <returns></returns>
        public virtual ITypable GetInvokeReutrnType() { return this.Object; }

        /// <summary>
        /// 执行
        /// </summary>
        public virtual void Invoke()
        {
            // 处理参数
            foreach (var p in Paramters)
            {
                // 参数内容处理
                switch (p)
                {
                    // 字符串对象
                    case SuValue<string> str:
                        IlMethod.Ldstr(new IlValue<string>(str.Value));
                        break;
                    // 变量
                    case SuField suField:
                        IlMethod.Ldloc_s(new IlName(suField.Name));
                        break;
                    default: throw new NotSupportedException($"Paramter '{p.GetType().FullName}' not supported.");
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
                // 兼容字段
                if (this.Object is SuStructType structType)
                {
                    if (structType.Base is SuField suField)
                    {
                        var field = suField.GetIlField();
                        invoker.IsStatic = field?.Keywords.Contains(SuKeys.Class) ?? false;
                    }
                }
            }
            // 添加返回类型
            if (this.ReturnType != null) invoker.Return(this.ReturnType.GetIlType());
            // 处理参数定义
            foreach (var type in Declares)
            {
                // 添加参数类型
                invoker.Param(type);
            }
            // 执行
            IlMethod.Call(invoker);
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns></returns>
        public string ToCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.IlMethod.Name}: ");
            if (this.Object is ICodable code)
            {
                sb.Append(code.ToCodeString());
            }
            else
            {
                sb.Append(this.Object.GetIlType().Name);
            }
            sb.Append('.');
            sb.Append(this.Name);
            if (Paramters.Any())
            {
                sb.Append("(");
                // 处理参数
                for (int i = 0; i < Paramters.Count; i++)
                {
                    if (i > 0) sb.Append(", ");
                    var p = Paramters[i];
                    if (p is ICodable pc)
                    {
                        sb.Append(pc.ToCodeString());
                    }
                    else
                    {
                        sb.Append(p.ToString());
                    }
                }
                sb.AppendLine(")");
            }
            else
            {
                sb.AppendLine("()");
            }
            return sb.ToString();
        }
    }
}
