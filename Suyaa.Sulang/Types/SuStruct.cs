using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su结构体
    /// </summary>
    public class SuStruct : NamedSuable, IStructable
    {
        /// <summary>
        /// 执行器
        /// </summary>
        public Dictionary<string, IInvokable> Invokers { get; }

        /// <summary>
        /// 所属对象
        /// </summary>
        public IInstantiable? Object { get; }

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="name"></param>
        public SuStruct(IInstantiable? obj, string name) : base(name)
        {
            this.Invokers = new Dictionary<string, IInvokable>();
            Object = obj;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IInvokable GetMethodInvoker(string name)
        {
            return this.Invokers[name];
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            if (this.Object is null) throw new SuException($"Object not found.");
            return this.Object.GetField(this.Name).Type;
        }
    }
}
