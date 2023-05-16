using Ssm.Jian.Engine;
using Suyaa;
using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Script.Apis.IO
{
    /// <summary>
    /// 控制台函数注册
    /// </summary>
    public class PathRegistr : ScriptRegistrBase
    {
        /// <summary>
        /// 路径合并
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("合并路径")]
        public string PathCombine(string path1, string path2)
            => sy.IO.CombinePath(path1, path2);

        /// <summary>
        /// 获取完整文件名
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("获取完整文件名")]
        public string PathGetFullFileName(string path)
            => Path.GetFileName(path);

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("获取文件名")]
        public string PathGetFileName(string path)
            => Path.GetFileNameWithoutExtension(path);

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("获取文件扩展名")]
        public string PathGetExtension(string path)
            => Path.GetExtension(path);

        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("获取父文件夹")]
        public string PathGetFolder(string path)
            => Path.GetDirectoryName(path).ToNotNull();
    }
}
