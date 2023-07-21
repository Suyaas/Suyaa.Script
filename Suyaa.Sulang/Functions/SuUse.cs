using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 使用语句
    /// </summary>
    public sealed class SuUse : SuMethod
    {
        /// <summary>
        /// 所属对象
        /// </summary>
        public SuGlobal Global { get; }

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
        public SuUse(SuGlobal sg, string name, IlType type) : base(sg, name)
        {
            Global = sg;
            Type = type;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke(IlMethod method)
        {
            if (this.Global.ContainsKey(this.Name))
            {
                throw new SuException($"Field '{this.Name}' is already exists.");
            }
            this.Global.Fields.Add(new IlField(this.Name, this.Type).Keyword(SuKeys.Class));
        }
    }
}
