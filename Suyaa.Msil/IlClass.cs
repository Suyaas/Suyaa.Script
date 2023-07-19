using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL类
    /// </summary>
    public class IlClass : NamedAssemblable
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public List<string> Keywords { get; }

        /// <summary>
        /// 方法集合
        /// </summary>
        public List<IlMethod> Methods { get; }

        /// <summary>
        /// 继承类
        /// </summary>
        public IAssemblableType? ExtendsClass { get; private set; }

        #region 快捷函数

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlClass Keyword(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Keywords.Contains(key)) this.Keywords.Add(key);
            }
            return this;
        }

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlClass Extends(IlExternType cls)
        {
            this.ExtendsClass = cls;
            return this;
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlMethod Method(string name)
        {
            var method = new IlMethod(new IlType(this.Name), name);
            this.Methods.Add(method);
            return method;
        }

        #endregion

        /// <summary>
        /// IL类
        /// </summary>
        /// <param name="name"></param>
        public IlClass(string name) : base(name)
        {
            this.Keywords = new List<string>();
            this.Methods = new List<IlMethod>();
        }

        /// <summary>
        /// 获取汇编指令
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(".class");
            foreach (var key in this.Keywords)
            {
                sb.Append(' ');
                sb.Append(key);
            }
            sb.Append(' ');
            sb.Append(this.Name);
            if (this.ExtendsClass != null)
            {
                sb.Append(" extends ");
                sb.Append(this.ExtendsClass.ToAssembly());
            }
            sb.AppendLine(" {");
            foreach (var method in this.Methods)
            {
                sb.AppendLine(method.ToAssembly());
            }
            sb.Append('}');
            return sb.ToString();
        }

        #region 释放资源

        protected override void OnManagedDispose()
        {
            // 释放可汇编对象
            this.Keywords.Clear();
            base.OnManagedDispose();
        }

        #endregion
    }
}
