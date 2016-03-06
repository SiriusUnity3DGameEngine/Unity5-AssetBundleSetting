///////////////////////////////////////////////////////////////////////////////
///
/// ExternalProcessorMenuItem.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

public class ExternalProcessorMenuItem
{
    /// <summary>
    /// Create ExternalProcessor.asset file.
    /// </summary>
    [MenuItem("Tools/AssetBundles/Create ExternalProcessor")]
    static public void CreateBuildSetting()
    {
        ExternalProcessor instance = ScriptableObject.CreateInstance<ExternalProcessor>();
        AssetDatabase.CreateAsset(instance, "Assets/Samples/ExternalProcessor/Scripts/Editor/ExternalProcessor.asset");
        AssetDatabase.SaveAssets();
    }
}