using Suyaa.Lang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Lang.Types
{
    /// <summary>
    /// 全局对象$
    /// </summary>
    public sealed class SuGlobal : Disposable, IInstantiable
    {
        /// <summary>
        /// 字段
        /// </summary>
        public List<IlField> Fields { get; }

        /// <summary>
        /// 全局对象$
        /// </summary>
        public SuGlobal()
        {
            this.Fields = new List<IlField>();
        }

        /// <summary>
        /// 检测字段名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsKey(string name)
        {
            return this.Fields.Where(d => d.Name == name).Any();
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlField this[string name]
        {
            get => this.Fields.Where(d => d.Name == name).FirstOrDefault() ?? throw new SuException($"Field '{name}' not exists.");
        }
    }
}
