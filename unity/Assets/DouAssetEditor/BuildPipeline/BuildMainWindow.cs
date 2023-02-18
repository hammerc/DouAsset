using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace BuildPipeline
{
    /// <summary>
    /// 打包主界面
    /// </summary>
    public class BuildMainWindow : OdinEditorWindow
    {
        [MenuItem("打包/打开打包主界面")]
        private static void OpenWindow()
        {
            var window = GetWindow<BuildMainWindow>();
            window.titleContent = new GUIContent("打包");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 800);
        }

        private bool _listChangedFlag = false;

        private BuildData _buildData;

        [LabelText("打包步骤")]
        [ListDrawerSettings]
        [Searchable]
        [OnCollectionChanged("OnStepListChangedBefore", "OnStepListChangedAfter")]
        public List<BuildStepData> stepList;

        public void OnStepListChangedBefore(CollectionChangeInfo info, List<BuildStepData> value)
        {
            _listChangedFlag = true;
        }

        public void OnStepListChangedAfter(CollectionChangeInfo info, List<BuildStepData> value)
        {
            _listChangedFlag = true;
        }

        [ShowIf("_listChangedFlag")]
        [Button("还原配置")]
        [HorizontalGroup("BtnGroup")]
        [GUIColor(1, 0, 0)]
        public void RestoreData()
        {
            ResetData();
            _listChangedFlag = false;
        }

        [ShowIf("_listChangedFlag")]
        [Button("保存配置")]
        [HorizontalGroup("BtnGroup")]
        [GUIColor(0, 1, 0)]
        public void SaveData()
        {
            BuildDataManager.instance.SaveBuildData(_buildData);
            ResetData();
            _listChangedFlag = false;
        }

        [Button("一键打包")]
        [GUIColor(1, 1, 0)]
        public void BuildAll()
        {
        }

        [OnInspectorInit]
        public void OnInspectorInit()
        {
            ResetData();
        }

        private void ResetData()
        {
            _buildData = BuildDataManager.instance.LoadBuildData();
            stepList = _buildData.stepList;
        }
    }
}
