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
    public class SuStructType : SuType, IStructable
    {
        // 基对象
        private readonly ITypable _obj;

        /// <summary>
        /// 执行器
        /// </summary>
        public List<SuMethodInfo> Methods { get; }

        /// <summary>
        /// 获取结构体的基实现
        /// </summary>
        public ITypable Base => _obj;

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="obj"></param>
        public SuStructType(ITypable obj) : base(obj.GetIlType())
        {
            this.Methods = new List<SuMethodInfo>();
            _obj = obj;
        }

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="type"></param>
        public SuStructType(IlType type) : base(type)
        {
            this.Methods = new List<SuMethodInfo>();
            _obj = new SuType(type);
        }

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="name"></param>
        public SuStructType(string name) : base(name)
        {
            this.Methods = new List<SuMethodInfo>();
            _obj = new SuType(base.GetIlType());
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual SuMethodInfo? GetMethod(string name)
        {
            return this.Methods.Where(d => d.Name == name).FirstOrDefault();
        }
    }
}
