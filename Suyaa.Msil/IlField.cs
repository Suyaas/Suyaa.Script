using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// Il字段
    /// </summary>
    public sealed class IlField : NamedAssemblable, IKeywordsable
    {
        /// <summary>
        /// 字段类型
        /// </summary>
        public IlType Type { get; }

        /// <summary>
        /// 描述关键字
        /// </summary>
        public List<string> Keywords { get; }

        #region 快捷函数

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlField Keyword(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Keywords.Contains(key)) this.Keywords.Add(key);
            }
            return this;
        }

        #endregion

        /// <summary>
        /// Il字段
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public IlField(string name, IlType type) : base(name)
        {
            Type = type;
            this.Keywords = new List<string>();
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $"{this.Type.ToAssembly()} {this.Name}";
        }
    }
}
