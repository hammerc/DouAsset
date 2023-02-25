using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BuildPipeline
{
    /// <summary>
    /// 打包配置数据
    /// </summary>
    public class BuildData : ScriptableObject
    {
        public List<BuildStepData> stepList;
    }

    [Serializable]
    public class BuildStepData
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        [LabelText("启用")]
        [HorizontalGroup("HGroup", LabelWidth = 35, Width = 50, PaddingLeft = 10)]
        public bool active = true;
        
        /// <summary>
        /// 配置步骤类型
        /// </summary>
        [LabelText("类型")]
        [HorizontalGroup("HGroup", LabelWidth = 35, Width = 150, PaddingLeft = 10)]
        [EnableIf("active")]
        public BuildSteps step;

        /// <summary>
        /// 构建平台
        /// </summary>
        [LabelText("平台")]
        [HorizontalGroup("HGroup", LabelWidth = 35, Width = 150, PaddingLeft = 10)]
        [EnableIf("active")]
        public BuildPlatform platform = BuildPlatform.All;
        
        /// <summary>
        /// 步骤描述
        /// </summary>
        [LabelText("描述")]
        [HorizontalGroup("HGroup", LabelWidth = 35, PaddingLeft = 10)]
        [EnableIf("active")]
        public string desc;

        /// <summary>
        /// 配置项具体数据文件路径
        /// </summary>
        [HideInInspector]
        public string dataPath;

        [OnInspectorInit]
        public void OnInspectorInit()
        {
            if (dataPath == null)
            {
                var name = $"Build_{Guid.NewGuid().ToString()}";
                dataPath = BuildDef.BuildConfigDir + name + ".asset";
                var type = BuildStepUtil.GetStepDataClass(step);
                BuildUtil.CreateSerializeFile(type, dataPath);
            }
        }

        [Button("配置")]
        [HorizontalGroup("HGroup", Width = 60, PaddingLeft = 10)]
        [EnableIf("active")]
        public void Setting()
        {
        }

        [Button("执行")]
        [HorizontalGroup("HGroup", Width = 60, PaddingLeft = 10, PaddingRight = 10)]
        [EnableIf("active")]
        public void Apply()
        {
        }

        [Button("复制")]
        [HorizontalGroup("HGroup", Width = 60, PaddingLeft = 10, PaddingRight = 10)]
        [EnableIf("active")]
        public void Duplicate()
        {
        }
    }
}
