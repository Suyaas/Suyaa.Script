﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Script
{
    /// <summary>
    /// 脚本函数注册器接口
    /// </summary>
    public interface IScriptRegistr
    {
        /// <summary>
        /// 设置脚本引擎
        /// </summary>
        /// <param name="engine"></param>
        void SetEngine(ScriptEngine? engine);
    }
}
