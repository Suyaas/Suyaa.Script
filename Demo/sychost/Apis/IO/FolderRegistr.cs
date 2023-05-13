using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sychost.Apis.IO
{
    /// <summary>
    /// 控制台函数注册
    /// </summary>
    public class FolderRegistr : ScriptRegistrBase
    {
        /// <summary>
        /// 路径合并
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public List<string> Folders(string path)
            => sy.IO.GetFolders(path).ToList();

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public bool FolderExists(string path)
            => sy.IO.FolderExists(path);

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public void FolderCreate(string path)
            => sy.IO.CreateFolder(path);

        /// <summary>
        /// 移动目录
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        [Func]
        public void FolderMove(string path1, string path2)
            => Directory.Move(path1, path2);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public void FolderDelete(string path)
            => Directory.Delete(path);

        /// <summary>
        /// 删除目录及所有子目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public void FolderDeleteAll(string path)
            => Directory.Delete(path, true);
    }
}
