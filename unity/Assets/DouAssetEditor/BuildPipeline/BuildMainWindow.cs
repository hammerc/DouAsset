using System;
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
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
        }

        private bool _listChangedFlag = false;
        private bool _supportPlatform = false;
        private bool _nonsupportPlatform = false;
        private string _supportBuildInfo;
        private string _nonsupportBuildInfo;
        
        private BuildData _buildData;

        [InfoBox("$_supportBuildInfo", visibleIfMemberName:"_supportPlatform", InfoMessageType = InfoMessageType.Info)]
        [InfoBox("$_nonsupportBuildInfo", visibleIfMemberName:"_nonsupportPlatform", InfoMessageType = InfoMessageType.Error)]
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
            
            var currentPlatform = BuildPipelineManager.instance.GetCurrentPlatform();
            _supportPlatform = currentPlatform != BuildPlatform.Nonsupport;
            _nonsupportPlatform = currentPlatform == BuildPlatform.Nonsupport;
            _supportBuildInfo = $"当前平台[{Enum.GetName(typeof(BuildPlatform), currentPlatform)}]支持打包。";
            _nonsupportBuildInfo = "当前平台不支持打包，请切换目标平台。";
        }

        private void ResetData()
        {
            _buildData = BuildDataManager.instance.LoadBuildData();
            stepList = _buildData.stepList;
        }
    }
}
