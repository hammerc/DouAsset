using System.IO;
using UnityEditor;
using UnityEngine;

namespace BuildPipeline
{
    /// <summary>
    /// 构建数据管理器
    /// </summary>
    public class BuildDataManager
    {
        private static BuildDataManager _instance;
        public static BuildDataManager instance => _instance ??= new BuildDataManager();

        private BuildData _buildData;

        private BuildDataManager()
        {
        }

        public BuildData LoadBuildData()
        {
            if (!File.Exists(BuildDef.BuildDataPath))
            {
                BuildUtil.CreateDirectory(BuildDef.BuildConfigDir);
                BuildUtil.CreateSerializeFile<BuildData>(BuildDef.BuildDataPath);
            }

            if (_buildData == null)
            {
                _buildData = AssetDatabase.LoadAssetAtPath<BuildData>(BuildDef.BuildDataPath);
            }

            // 切断和资源文件的连接
            return ScriptableObject.Instantiate(_buildData);
        }

        public void SaveBuildData(BuildData buildData)
        {
            BuildUtil.SetData(_buildData, buildData);
            EditorUtility.SetDirty(_buildData);
            AssetDatabase.SaveAssetIfDirty(_buildData);
        }
    }
}
