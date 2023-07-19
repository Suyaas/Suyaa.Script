using Suyaa.Msil.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Helpers
{
    /// <summary>
    /// 平台信息助手
    /// </summary>
    public static class PlatfromsHelper
    {
        /// <summary>
        /// 转化为平台标识字符串
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static string ToPlatfromString(this Platforms platform)
        {
            return platform switch
            {
                Platforms.Win_x86 => "win-x86",
                Platforms.Win_x64 => "win-x64",
                Platforms.Win_Arm => "win-arm",
                Platforms.Win_Arm64 => "win-arm64",
                Platforms.OSX_x64 => "osx-x64",
                Platforms.OSX_Arm64 => "osx-arm64",
                Platforms.Linux_x64 => "linux-x64",
                Platforms.Linux_Arm => "linux-arm",
                Platforms.Linux_Arm64 => "linux-arm64",
                _ => throw new MsilException($"Unsupported platform '{platform}'."),
            };
        }
    }
}
