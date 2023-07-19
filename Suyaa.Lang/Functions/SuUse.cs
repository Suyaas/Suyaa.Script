using Suyaa.Lang.Exceptions;
using Suyaa.Lang.Types;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Lang.Functions
{
    /// <summary>
    /// 使用语句
    /// </summary>
    public class SuUse : Disposable, IInvokable
    {
        /// <summary>
        /// 所属对象
        /// </summary>
        public SuGlobal Global { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 类型
        /// </summary>
        public IlType Type { get; }

        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="sg"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public SuUse(SuGlobal sg, string name, IlType type)
        {
            Global = sg;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Invoke()
        {
            if (this.Global.ContainsKey(this.Name))
            {
                throw new SuException($"Field '{this.Name}' is already exists.");
            }
            this.Global.Fields.Add(new IlField(this.Name, this.Type));
        }
    }
}
