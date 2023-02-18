using System;

namespace BuildPipeline
{
    /// <summary>
    /// 构建步骤常量定义
    /// </summary>
    public static class BuildStepUtil
    {
        public static Type GetStepDataClass(BuildSteps step)
        {
            switch (step)
            {
                case BuildSteps.BundlePolicy:
                    return typeof(BundlePolicyData);
            }
            return null;
        }
    }
}
