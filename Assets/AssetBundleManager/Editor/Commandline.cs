using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// A helper class which enables to build assetbundles via command-line options.
/// </summary>
public class Commandline
{
    /// <summary>
    /// Call with command-line options to build assetbundles.
    /// </summary>
    public static void BuildAssetbundles()
    {
        string settingPath = "Assets/AssetBundleManager/Editor/AssetBundleBuildSetting.asset";
        AssetBundles.BundleBuilder builder = (AssetBundles.BundleBuilder)AssetDatabase.LoadAssetAtPath(settingPath, typeof(AssetBundles.BundleBuilder));
        if (builder != null)
        {
            builder.Build();
        }
        else
        {
            EditorUtility.DisplayDialog("AssetBundle Build Error", "Failed to load builder", "ok");
        }
    }
}
