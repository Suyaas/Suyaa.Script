using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        public List<SuMethodInfo> Methods { get; }

        /// <summary>
        /// 所属对象
        /// </summary>
        public IInstantiable? Object { get; }

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public SuStruct(IInstantiable? obj, string name) : base(name)
        {
            this.Methods = new List<SuMethodInfo>();
            Object = obj;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SuMethodInfo GetMethod(string name)
        {
            return this.Methods.Where(d => d.Name == name).First();
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            if (this.Object is null) return new IlType(nameof(SuGlobal));
            return this.Object.GetField(this.Name).Type;
        }
    }
}
