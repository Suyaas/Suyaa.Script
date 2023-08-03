using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL外部类
    /// </summary>
    public class IlType : NamedAssemblable, IAssemblableType, IKeywordsable
    {
        private static IlType? _type;

        /// <summary>
        /// 静态类型
        /// </summary>
        public static IlType Type { get => _type ??= new IlType(nameof(IlType)); }

        /// <summary>
        /// 关键字
        /// </summary>
        public List<string> Keywords { get; }

        #region 快捷函数

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlType Keyword(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Keywords.Contains(key)) this.Keywords.Add(key);
            }
            return this;
        }

        #endregion

        /// <summary>
        /// IL外部类
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        public IlType(string name) : base(name)
        {
            this.Keywords = new List<string>();
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Keywords)
            {
                sb.Append(key);
                sb.Append(' ');
            }
            sb.Append(this.Name);
            return sb.ToString();
        }
    }
}
