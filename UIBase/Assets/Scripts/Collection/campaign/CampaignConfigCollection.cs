using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class CampaignStageData
{
    public int stage;
    public Reward[] rewards;
}

[System.Serializable]
public class CampaignConfigCollection : ScriptableObject
{
    public CampaignStageData[] dataGroups;

    public CampaignWorldConfig worldConfig;

    public bool IsPassWorldWithId(int stage)
    {
        var modeId = CampaignModeConfig.GetModeId(stage);
        var mode = GetModeCampaignWithId(modeId);
        return mode.IsPassWorldMap(stage);
    }

    public CampaignStageData GetNextStage(int currentStage)
    {
        var modeId = CampaignModeConfig.GetModeId(currentStage);

        // check mode with current stage
        var mode = GetModeCampaignWithId(modeId);
        if (mode == null) return null;

        // check map with current stage
        var mapId = CampaignMapConfig.GetMapId(currentStage);

        var map = mode.GetMapWithId(mapId);
        if (map == null) return null;

        var stage = map.GetNextStage(currentStage);
        if (stage != null)
        {
            return stage;
        }
        else
        {
            modeId += 1;
            mode = GetModeCampaignWithId(modeId);
            if (mode == null) return null;

            map = mode.GetMapWithId(1);
            if (map == null) return null;

            stage = map.GetNextStage(currentStage);
            if (stage != null) return stage;
        }

        return null;
    }

    public CampaignModeConfig GetModeCampaignWithId(int id)
    {
        return worldConfig.GetModeCampaignWithId(id);
    }

    #region Parse data

    public void SetupWorldConfig()
    {
        if (dataGroups.Length > 0)
        {
            var modeCount = CampaignModeConfig.GetModeId(dataGroups[dataGroups.Length - 1].stage);

            var modeConfigList = new CampaignWorldConfig();
            for (int i = 0; i < modeCount; i++)
            {
                var mode = new CampaignModeConfig();
                mode.modeId = i + 1;

                var mapElement = new CampaignMapConfig();
                mapElement.mapId = 1;
                mode.mapList.Add(mapElement);

                for (int j = 0; j < dataGroups.Length; j++)
                {
                    var data = dataGroups[j];
                    var modeId = CampaignModeConfig.GetModeId(data.stage);

                    if (modeId == mode.modeId)
                    {
                        var mapId = CampaignMapConfig.GetMapId(data.stage);
                        if (mapId == mapElement.mapId)
                        {
                            mapElement.stageList.Add(data);
                        }
                        else
                        {
                            mapElement = new CampaignMapConfig();
                            mapElement.mapId = mapId;
                            mapElement.stageList.Add(data);
                            mode.mapList.Add(mapElement);
                        }
                    }
                }

                modeConfigList.modeConfigList.Add(mode);
            }

            worldConfig = modeConfigList;
        }
    }

    #endregion
}


[System.Serializable]
public class CampaignWorldConfig
{
    public List<CampaignModeConfig> modeConfigList = new List<CampaignModeConfig>();

    public CampaignModeConfig GetModeCampaignWithId(int id)
    {
        for (int i = 0; i < modeConfigList.Count; i++)
        {
            if (id == modeConfigList[i].modeId) return modeConfigList[i];
        }

        return null;
    }
}

[System.Serializable]
public class CampaignModeConfig
{
    public int modeId;
    public List<CampaignMapConfig> mapList = new List<CampaignMapConfig>();

    public bool IsPassWorldMap(int stage)
    {
        var count = mapList.Count;
        if (count > 0)
        {
            var map = this.mapList[count - 1];
            if (map.stageList.Count > 0)
            {
                var maxStage = map.stageList[map.stageList.Count - 1];
                return stage > maxStage.stage;
            }
        }

        return false;
    }

    public CampaignMapConfig GetMapWithId(int mapId)
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapId == mapList[i].mapId) return mapList[i];
        }

        return null;
    }
    
    public static int GetModeId(int stageId)
    {
        return stageId / 100000;
    }
}


[System.Serializable]
public class CampaignMapConfig
{
    public int mapId;
    public List<CampaignStageData> stageList = new List<CampaignStageData>();

    public CampaignStageData GetNextStage(int stage)
    {
        for (int i = 0; i < stageList.Count; i++)
        {
            if (stage < stageList[i].stage) return stageList[i];
        }

        return null;
    }

    public CampaignStageData GetStageWithId(int stage)
    {
        for (int i = 0; i < stageList.Count; i++)
        {
            if (stage == stageList[i].stage) return stageList[i];
        }

        return null;
    }
    
    public static int GetMapId(int stageId)
    {
        return stageId % 100000 / 1000;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CampaignConfigCollection))]
[CanEditMultipleObjects]
public class CampaignConfigEditor : Editor
{
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt()
    {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    }

    public override void OnInspectorGUI()
    {
        CampaignConfigCollection myscript = (CampaignConfigCollection) target;

        if (GUILayout.Button("Load World Config"))
        {
            myscript.SetupWorldConfig();
            EditorUtility.SetDirty(myscript);
        }

        base.OnInspectorGUI();
    }
}
#endif