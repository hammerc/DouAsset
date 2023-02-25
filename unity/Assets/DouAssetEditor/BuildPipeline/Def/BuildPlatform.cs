using System;

namespace BuildPipeline
{
    /// <summary>
    /// 构建平台
    /// </summary>
    [Flags]
    public enum BuildPlatform
    {
        /// <summary>
        /// 未支持
        /// </summary>
        Nonsupport = 0,
        
        /// <summary>
        /// Win32
        /// </summary>
        Windows = 1,
        
        /// <summary>
        /// Win64
        /// </summary>
        Windows_64 = 1 << 1,
        
        /// <summary>
        /// Android
        /// </summary>
        Android = 1 << 2,
        
        /// <summary>
        /// iOS
        /// </summary>
        iOS = 1 << 3,
        
        /// <summary>
        /// 全平台
        /// </summary>
        All = Windows | Windows_64 | Android | iOS
    }
}
