using System;

namespace Suyaa.Msil
{
    /// <summary>
    /// 平台
    /// </summary>
    public enum Platforms : int
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// Win_x86
        /// </summary>
        Win_x86 = 0x0101,
        /// <summary>
        /// Win_x64
        /// </summary>
        Win_x64 = 0x0102,
        /// <summary>
        /// Win_Arm
        /// </summary>
        Win_Arm = 0x0111,
        /// <summary>
        /// Win_Arm64
        /// </summary>
        Win_Arm64 = 0x0112,
        /// <summary>
        /// OSX_x64
        /// </summary>
        OSX_x64 = 0x0202,
        /// <summary>
        /// OSX_Arm64
        /// </summary>
        OSX_Arm64 = 0x0212,
        /// <summary>
        /// Linux_x64
        /// </summary>
        Linux_x64 = 0x0302,
        /// <summary>
        /// Linux_Arm
        /// </summary>
        Linux_Arm = 0x0311,
        /// <summary>
        /// Linux_Arm64
        /// </summary>
        Linux_Arm64 = 0x0312,
    }
}
