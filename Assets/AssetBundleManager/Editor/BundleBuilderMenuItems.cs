///////////////////////////////////////////////////////////////////////////////
///
/// BundleBuilderMenuItems.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AssetBundles
{
    public class BundleBuilderMenuItems
    {
        [MenuItem("Tools/AssetBundles/Create Build Setting")]
        static public void CreateBuildSetting()
        {
            BundleBuilder.CreateBuildSetting();
        }
    }
}