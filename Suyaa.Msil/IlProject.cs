using Suyaa.Msil.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// Msil项目
    /// </summary>
    public sealed class IlProject : Disposable
    {
        /// <summary>
        /// 可汇编对象集
        /// </summary>
        public List<IAssemblable> Assemblables { get; }

        /// <summary>
        /// Msil项目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        public IlProject(string name, string folder)
        {
            Name = name;
            Folder = folder;
            this.Assemblables = new List<IAssemblable>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 目录
        /// </summary>
        public string Folder { get; }

        #region MSIL操作

        /// <summary>
        /// 添加一个程序集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlAssembly Assembly(string name)
        {
            var assembly = new IlAssembly(name);
            this.Assemblables.Add(assembly);
            return assembly;
        }

        /// <summary>
        /// 添加一个扩展程序集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlExternAssembly ExternAssembly(string name)
        {
            var assembly = new IlExternAssembly(name);
            this.Assemblables.Add(assembly);
            return assembly;
        }

        /// <summary>
        /// 添加一个扩展程序集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T ExternAssembly<T>()
            where T : IlExternAssembly
        {
            var assembly = sy.Assembly.Create<T>();
            this.Assemblables.Add(assembly);
            return assembly;
        }


        /// <summary>
        /// 添加一个类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlClass Class(string name)
        {
            var cls = new IlClass(name);
            this.Assemblables.Add(cls);
            return cls;
        }

        #endregion

        /// <summary>
        /// 输出
        /// </summary>
        public void Output()
        {
            // 创建项目文件
            string projContent = @"
                <Project Sdk=""Microsoft.Net.Sdk.il/7.0.0"">
                  <PropertyGroup>
                    <OutputType>Exe</OutputType>
                    <TargetFramework>net7.0</TargetFramework>
                  </PropertyGroup>
                  <ItemGroup>
                    <None Include=""Program.il"" />
                  </ItemGroup>
                </Project>
            ";
            // 输出项目文件
            string projPath = sy.IO.CombinePath(this.Folder, this.Name + ".ilproj");
            sy.IO.WriteUtf8FileContent(projPath, projContent);
            // 输出程序文件
            StringBuilder il = new StringBuilder();
            foreach (var item in Assemblables)
            {
                il.AppendLine(item.ToAssembly());
            }
            string ilPath = sy.IO.CombinePath(this.Folder, "Program.il");
            sy.IO.WriteUtf8FileContent(ilPath, il.ToString());
            // 清理数据
            il.Clear();
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public string Build(string? path = null)
        {
            using Terminal terminal = new Terminal(this.Folder);
            StringBuilder sb = new StringBuilder();
            sb.Append("build");
            if (!path.IsNullOrWhiteSpace())
            {
                sb.Append(" -o ");
                sb.Append('"');
                sb.Append(path);
                sb.Append('"');
            }
            return terminal.Execute("dotnet", sb.ToString());
        }

        /// <summary>
        /// 编译并发布
        /// </summary>
        /// <returns></returns>
        public string Release(string? path = null)
        {
            using Terminal terminal = new Terminal(this.Folder);
            StringBuilder sb = new StringBuilder();
            sb.Append("build -c release");
            // 添加输出目录
            if (!path.IsNullOrWhiteSpace())
            {
                sb.Append(" -o ");
                sb.Append('"');
                sb.Append(path);
                sb.Append('"');
            }
            return terminal.Execute("dotnet", sb.ToString());
        }

        /// <summary>
        /// 编译并发布
        /// </summary>
        /// <returns></returns>
        public string Release(Platforms platform, string? path = null)
        {
            using Terminal terminal = new Terminal(this.Folder);
            StringBuilder sb = new StringBuilder();
            sb.Append("build -c release");
            // 添加平台信息
            sb.Append(" -r ");
            sb.Append(platform.ToPlatfromString());
            // 添加输出目录
            if (!path.IsNullOrWhiteSpace())
            {
                sb.Append(" -o ");
                sb.Append('"');
                sb.Append(path);
                sb.Append('"');
            }
            return terminal.Execute("dotnet", sb.ToString());
        }

        /// <summary>
        /// 以Aot方式编译并发布
        /// </summary>
        /// <returns></returns>
        public string Publish(Platforms platform, bool isAot, string? path = null)
        {
            using Terminal terminal = new Terminal(this.Folder);
            StringBuilder sb = new StringBuilder();
            sb.Append("publish -c release --self-contained /p:PublishTrimmed=true");
            // 添加Aot支持
            if (isAot)
            {
                sb.Append(" /p:PublishAot=true");
            }
            else
            {
                sb.Append(" /p:PublishSingleFile=true");
            }
            // 添加平台信息
            sb.Append(" -r ");
            sb.Append(platform.ToPlatfromString());
            // 添加输出目录
            if (!path.IsNullOrWhiteSpace())
            {
                sb.Append(" -o ");
                sb.Append('"');
                sb.Append(path);
                sb.Append('"');
            }
            return terminal.Execute("dotnet", sb.ToString());
        }

        #region 释放资源

        protected override void OnManagedDispose()
        {
            // 释放可汇编对象
            for (int i = 0; i < this.Assemblables.Count; i++) this.Assemblables[i].Dispose();
            this.Assemblables.Clear();
            base.OnManagedDispose();
        }

        #endregion
    }
}
