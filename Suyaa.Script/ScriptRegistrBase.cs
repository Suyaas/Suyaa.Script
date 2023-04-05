using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Script
{
    /// <summary>
    /// 脚本函数注册器基础类
    /// </summary>
    public abstract class ScriptRegistrBase : IScriptRegistr
    {
        // 关联引擎
        private ScriptEngine? _engine;

        /// <summary>
        /// 设置脚本引擎
        /// </summary>
        /// <param name="engine"></param>
        public void SetEngine(ScriptEngine? engine)
        {
            _engine = engine;
        }

        /// <summary>
        /// 获取关联引擎
        /// </summary>
        public ScriptEngine Engine => _engine ?? throw new ScriptException($"关联引擎为空");
    }
}
