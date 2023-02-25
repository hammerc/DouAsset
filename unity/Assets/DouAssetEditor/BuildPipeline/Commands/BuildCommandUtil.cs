using System;

namespace BuildPipeline
{
    /// <summary>
    /// 构建命令常量定义
    /// </summary>
    public static class BuildCommandUtil
    {
        public static Type GetCommandDataClass(BuildCommands commands)
        {
            switch (commands)
            {
                case BuildCommands.BundlePolicy:
                    return typeof(BundlePolicyData);
            }
            return null;
        }
    }
}
