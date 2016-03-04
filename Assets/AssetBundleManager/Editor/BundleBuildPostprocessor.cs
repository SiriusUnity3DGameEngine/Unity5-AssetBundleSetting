using UnityEngine;
using UnityEditor;

namespace AssetBundles
{
    /// <summary>
    /// A postprocessor to call a callback when the AssetBundle an asset is associated with changes.
    /// </summary>
    public class BundleBuildPostprocessor : AssetPostprocessor
    {
        void OnPostprocessAssetbundleNameChanged(string path, string previous, string next)
        {
            Debug.Log("AssetBundles: " + path + " old: " + previous + " new: " + next);
        }
    }
}