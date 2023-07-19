using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 可释放对象
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        #region 释放资源

        // 是否已经执行释放
        private bool _disposed = false;

        /// <summary>
        /// 释放托管资源
        /// </summary>
        protected virtual void OnManagedDispose() { }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        protected virtual void OnUnmanagedDispose() { }

        // 释放资源
        private void OnDisposed(bool isManaged)
        {
            if (_disposed) return;
            // 释放托管资源
            if (isManaged)
            {
                OnManagedDispose();
            }
            // 释放非托管资源
            OnUnmanagedDispose();
            _disposed = true;
        }

        /// <summary>
        /// 主动释放
        /// </summary>
        public void Dispose()
        {
            OnDisposed(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 构析函数
        /// </summary>
        ~Disposable()
        {
            OnDisposed(false);
        }

        #endregion
    }
}
