﻿///////////////////////////////////////////////////////////////////////////////
///
/// BundleBuilder.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace AssetBundles
{
    /// <summary>
    /// ScriptableObject class which provides assetbundle build options.
    /// 
    /// Note:
    ///     BuildAssetBundleOptions
    ///         * CollectDependencies and DeterministicAssetBundle are always enabled.
    ///         * CompleteAssets is ingored as we always start from assets rather than objects, it should be complete by default.
    ///         * ForceRebuildAssetBundle is added. Even there is no change to the assets, you can force rebuild the AssetBundles by setting this flag.
    ///         * IngoreTypeTreeChanges is added. Even type tree changes, you can ignore the type tree changes with this flag.
    ///         * DisableWriteTypeTree conflicts with IngoreTypeTreeChanges. You can’t ignore type tree changes if you disable type tree.
    ///         
    /// See the following for more details:
    ///     http://docs.unity3d.com/500/Documentation/Manual/BuildingAssetBundles5x.html
    ///     
    /// </summary>
    public class BundleBuilder : ScriptableObject
    {

        // used seirializable dictionary to serialize assetbundle options.
        [Serializable]
        public class OptionDictionary : SerializableDictionary<BuildAssetBundleOptions, bool> { }
        public OptionDictionary EnabledOptions;

        // A place where to put the assetbundles.
        public string outputPath = string.Empty;

        
        void OnEnable()
        {
            if (EnabledOptions == null)
                EnabledOptions = new OptionDictionary();

            foreach (string name in Enum.GetNames(typeof(BuildAssetBundleOptions)))
            {
                // the first enum value is 'None' so we skip it.
                if (string.IsNullOrEmpty(name) || name == "None")
                    continue;

                BuildAssetBundleOptions key = (BuildAssetBundleOptions)Enum.Parse(typeof(BuildAssetBundleOptions), name);
                if (!EnabledOptions.ContainsKey(key))
                {
                    bool val = false;
#if UNITY_5
                    // It is always enabled in the new AssetBundle build system introduced in 5.0.
                    if (key != BuildAssetBundleOptions.CollectDependencies &&
                        key != BuildAssetBundleOptions.CompleteAssets &&
                        key != BuildAssetBundleOptions.DeterministicAssetBundle)
                    {
                        EnabledOptions.Add(key, val);
                    }
#else
                    EnabledOptions.Add(key, val);
#endif
                }
            }
        }

        static public string CreateAssetBundleDirectory(string rootFolder)
        {
            if (string.IsNullOrEmpty(rootFolder))
                rootFolder = Utility.AssetBundlesOutputPath;

            // Choose the output path according to the build target.
            string outputPath = Path.Combine(rootFolder, Utility.GetPlatformName());
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            return outputPath;
        }

        /// <summary>
        /// Export AssetsBundles under the specified output path.
        /// </summary>
        public void Build()
        {
            // Choose the output path according to the build target.
            outputPath = CreateAssetBundleDirectory(outputPath);

            // Specifies assetbundle build options.
            var options = BuildAssetBundleOptions.None;
            foreach (KeyValuePair<BuildAssetBundleOptions, bool> enabled in EnabledOptions)
            {
                if (enabled.Key == BuildAssetBundleOptions.None)
                    continue;

                if (enabled.Value)
                    options |= enabled.Key;
            }

            //TODO: later, may be...
            /*
            bool shouldCheckODR = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
#if UNITY_TVOS
            shouldCheckODR |= EditorUserBuildSettings.activeBuildTarget == BuildTarget.tvOS;
#endif
            if (shouldCheckODR)
            {
#if ENABLE_IOS_ON_DEMAND_RESOURCES
                if (PlayerSettings.iOS.useOnDemandResources)
                    options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif
#if ENABLE_IOS_APP_SLICING
                options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif
            }
            */ 

            // Build assetbundles.
            BuildPipeline.BuildAssetBundles(outputPath, options, EditorUserBuildSettings.activeBuildTarget);
        }

        public static void CreateBuildSetting()
        {
            BundleBuilder instance = ScriptableObject.CreateInstance<BundleBuilder>();
            AssetDatabase.CreateAsset(instance, "Assets/AssetBundleManager/Editor/AssetBundleBuildSetting.asset");
            AssetDatabase.SaveAssets();
        }
    }

}