using Ssm.Jian.Engine;
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
    public class FolderRegistr : ScriptRegistrBase
    {
        /// <summary>
        /// 路径合并
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("获取子文件夹列表")]
        public List<string> Folders(string path)
            => sy.IO.GetFolders(path).ToList();

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("检测文件夹")]
        public bool FolderExists(string path)
            => sy.IO.FolderExists(path);

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("创建文件夹")]
        public void FolderCreate(string path)
            => sy.IO.CreateFolder(path);

        /// <summary>
        /// 移动目录
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("移动文件夹")]
        public void FolderMove(string path1, string path2)
            => Directory.Move(path1, path2);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("删除文件夹")]
        public void FolderDelete(string path)
            => Directory.Delete(path);

        /// <summary>
        /// 删除目录及所有子目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("删除文件夹及子项")]
        public void FolderDeleteAll(string path)
            => Directory.Delete(path, true);
    }
}
