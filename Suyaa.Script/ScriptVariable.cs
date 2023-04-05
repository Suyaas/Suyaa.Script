using System;
using System.Collections.Generic;

namespace Suyaa.Script
{
    /// <summary>
    /// 变量
    /// </summary>
    public class ScriptVariable : IDisposable
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
