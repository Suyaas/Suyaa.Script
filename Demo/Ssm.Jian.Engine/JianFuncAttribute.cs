using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ssm.Jian.Engine
{
    /// <summary>
    /// 声声慢·简 函数定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class JianFuncAttribute : FuncAttribute
    {
        /// <summary>
        /// 声声慢·简 函数定义
        /// </summary>
        /// <param name="name">名称</param>
        public JianFuncAttribute(string name) : base(name) { }
    }
}
