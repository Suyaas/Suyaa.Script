﻿using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sychost.Apis.IO
{
    /// <summary>
    /// 文件函数注册
    /// </summary>
    public class FileRegistr : ScriptRegistrBase
    {
        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public List<string> Files(string path)
            => sy.IO.GetFiles(path).ToList();

        /// <summary>
        /// 查找文件
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public List<string> FilesFind(string path, string pattern)
            => sy.IO.GetFiles(path, pattern).ToList();

        /// <summary>
        /// 查找所有文件
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public List<string> FilesFindAll(string path, string pattern)
        {
            List<string> paths = FilesFind(path, pattern);
            var folders = sy.IO.GetFolders(path);
            foreach (var folder in folders)
            {
                paths.AddRange(FilesFindAll(folder, pattern));
            }
            return paths;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public bool FileExists(string path)
            => sy.IO.FileExists(path);

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        [Func]
        public void FileCopy(string path1, string path2)
            => File.Copy(path1, path2);

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        [Func]
        public void FileMove(string path1, string path2)
            => File.Move(path1, path2);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Func]
        public bool FileDelete(string path)
            => sy.IO.DeleteFile(path);
    }
}
