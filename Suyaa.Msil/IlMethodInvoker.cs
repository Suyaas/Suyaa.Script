using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL方法
    /// </summary>
    public class IlMethodInvoker : NamedAssemblable, IAssemblableInvoker
    {
        /// <summary>
        /// 所属类
        /// </summary>
        public IlType ClassType { get; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public IlType? ReturnType { get; private set; }

        /// <summary>
        /// 是否静态方法
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<IlType> Paramters { get; }

        #region 快捷函数

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlMethodInvoker Param(params IlType[] paramters)
        {
            this.Paramters.AddRange(paramters.ToList());
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <returns></returns>
        public IlMethodInvoker Param<T>()
            where T : IlType
        {
            this.Paramters.Add(sy.Assembly.Create<T>());
            return this;
        }

        /// <summary>
        /// 设置返回类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IlMethodInvoker Return(IlType type)
        {
            this.ReturnType = type;
            return this;
        }

        #endregion

        /// <summary>
        /// IL方法
        /// </summary>
        /// <param name="name"></param>
        public IlMethodInvoker(IlType type, string name) : base(name)
        {
            this.IsStatic = false;
            this.ClassType = type;
            this.Paramters = new List<IlType>();
        }

        /// <summary>
        /// 获取汇编指令
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            StringBuilder sb = new StringBuilder();
            // 添加返回类型
            if (this.ReturnType is null)
            {
                sb.Append(IlKeys.Void);
            }
            else
            {
                sb.Append(this.ReturnType.ToAssembly());
            }
            // 添加方法名
            sb.Append(' ');
            sb.Append(this.ClassType.ToAssembly());
            sb.Append(IsStatic ? "::" : ".");
            sb.Append(this.Name);
            // 添加参数信息
            sb.Append("(");
            bool isFirstField = true;
            foreach (var type in this.Paramters)
            {
                if (isFirstField)
                {
                    isFirstField = false;
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(type.ToAssembly());
            }
            sb.Append(')');
            return sb.ToString();
        }
    }
}
