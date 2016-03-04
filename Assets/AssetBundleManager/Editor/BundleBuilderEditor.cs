///////////////////////////////////////////////////////////////////////////////
///
/// BundleBuilder.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AssetBundles
{
    /// <summary>
    /// Custom inspector editor class to provide assetbundle build options.
    /// </summary>
    [CustomEditor(typeof(BundleBuilder))]
    public class BundleBuilderEditor : Editor
    {
        BundleBuilder builder;

        void OnEnable()
        {
            builder = target as BundleBuilder;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("AssetBundle Options:", EditorStyles.boldLabel);
            string[] names = Enum.GetNames(typeof(BuildAssetBundleOptions));
            for(int i=0; i<names.Length; i++)
            {
                if (string.IsNullOrEmpty(names[i]) || names[i] == "None")
                    continue;

                BuildAssetBundleOptions key = (BuildAssetBundleOptions)Enum.Parse(typeof(BuildAssetBundleOptions), names[i]);
                if (builder.EnabledOptions.ContainsKey(key))
                {
                    builder.EnabledOptions[key] = EditorGUILayout.ToggleLeft(" " + names[i], builder.EnabledOptions[key]);
                }
            }

#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2) // for the version of Unity over 5.3x
            bool uncompressedAssetBundle;
            if (builder.EnabledOptions.TryGetValue(BuildAssetBundleOptions.UncompressedAssetBundle, out uncompressedAssetBundle))
            {
                if (uncompressedAssetBundle)
                {
                    bool chunkBasedCompression;
                    if (builder.EnabledOptions.TryGetValue(BuildAssetBundleOptions.ChunkBasedCompression, out chunkBasedCompression))
                    {
                        if (chunkBasedCompression)
                        {
                            if (EditorUtility.DisplayDialog("NOTE",
                                "Force disable 'UncompressedAssetBundle' option due to the 'ChunkBasedCompression' option is enabled.", "Ok"))
                            {
                                builder.EnabledOptions[BuildAssetBundleOptions.UncompressedAssetBundle] = false;
                            }
                        }
                    }
                }
            }
#endif

#if UNITY_5
            // DisableWriteTypeTree conflicts with IngoreTypeTreeChanges. So you can’t enable both of the options.
            bool disableWriteTypeTree;
            if (builder.EnabledOptions.TryGetValue(BuildAssetBundleOptions.DisableWriteTypeTree, out disableWriteTypeTree))
            {
                if (disableWriteTypeTree)
                {
                    bool ignoreTypeTreeChanges;
                    if (builder.EnabledOptions.TryGetValue(BuildAssetBundleOptions.IgnoreTypeTreeChanges, out ignoreTypeTreeChanges))
                    {
                        if (ignoreTypeTreeChanges)
                        {
                            //builder.EnabledOptions[BuildAssetBundleOptions.IgnoreTypeTreeChanges] = false;
                            if (EditorUtility.DisplayDialog("NOTE",
                                "You can’t ignore type tree changes if you disable type tree.", "Ok"))
                            {
                                builder.EnabledOptions[BuildAssetBundleOptions.IgnoreTypeTreeChanges] = false;
                            }
                        }
                    }
                }
            }
#endif
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Use Default Setting", GUILayout.Width(150)))
                {
                    UseDefaultSetting();
                }
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Output Path:", EditorStyles.boldLabel);

            using (new EditorGUILayout.HorizontalScope())
            {
                builder.outputPath = GUILayout.TextField(builder.outputPath, GUILayout.MinWidth(250));
                if (GUILayout.Button("...", GUILayout.Width(20)))
                {
                    // unity editor expects the current folder to be set to the project folder at all times.
                    string projectFolder = System.IO.Directory.GetCurrentDirectory();
                    string path = string.Empty;
                    path = EditorUtility.OpenFolderPanel("Select folder", projectFolder, "");
                    if (path.Length != 0)
                    {
                        builder.outputPath = path;
                    }
                }
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("AssetBundle Build:", EditorStyles.boldLabel);
            if (GUILayout.Button("Build"))
            {
                //HACK: To prevent InvalidOperationException.
                //  See http://answers.unity3d.com/questions/852155/invalidoperationexception-operation-is-not-valid-d-1.html
                EditorApplication.delayCall += builder.Build;
            }
        }

        private void UseDefaultSetting()
        {
            // Invalidate all build options.
            foreach(BuildAssetBundleOptions key in builder.EnabledOptions.Keys)
            {
                builder.EnabledOptions[key] = false;
            }
        }
    }
}