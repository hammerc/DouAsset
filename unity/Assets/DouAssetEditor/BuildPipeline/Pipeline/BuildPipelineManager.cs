using UnityEditor;

namespace BuildPipeline
{
    /// <summary>
    /// 构建管线管理器
    /// </summary>
    public class BuildPipelineManager
    {
        private static BuildPipelineManager _instance;
        public static BuildPipelineManager instance => _instance ??= new BuildPipelineManager();

        private BuildPipelineManager()
        {
        }

        /// <summary>
        /// 获取当前编辑器处于的平台
        /// </summary>
        public BuildPlatform GetCurrentPlatform()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return BuildPlatform.Windows;
                case BuildTarget.StandaloneWindows64:
                    return BuildPlatform.Windows_64;
                case BuildTarget.Android:
                    return BuildPlatform.Android;
                case BuildTarget.iOS:
                    return BuildPlatform.iOS;
            }
            return BuildPlatform.Nonsupport;
        }
    }
}
