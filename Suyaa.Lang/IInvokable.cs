﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang
{
    /// <summary>
    /// 可执行对象
    /// </summary>
    public interface IInvokable : ISuable
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Invoke();
    }
}
