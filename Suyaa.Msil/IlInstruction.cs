using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// Il指令
    /// </summary>
    public class IlInstruction : NamedAssemblable
    {
        /// <summary>
        /// 参数
        /// </summary>
        public List<IAssemblable> Paramters { get; }

        /// <summary>
        /// Il指令
        /// </summary>
        /// <param name="name"></param>
        public IlInstruction(string name) : base(name)
        {
            this.Paramters = new List<IAssemblable>();
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            StringBuilder sb = new StringBuilder();
            // 添加指令
            sb.Append(this.Name);
            // 添加参数
            foreach (var paramter in this.Paramters)
            {
                sb.Append(' ');
                sb.Append(paramter.ToAssembly());
            }
            return sb.ToString();
        }
    }
}
