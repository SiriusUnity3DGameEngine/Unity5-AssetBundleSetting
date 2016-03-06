AssetBundleSetting Tool for Unity 5.x
================================

A tool which provides a simple editor setting to build assetbundles especially on Unity5.x.


![ setting](./Images/setting.png "setting")


It provides a simple editor tool to set various assetbundle options regardless of Unity's minor version so it works on any version of Unity5.x. and makes it easy to build assetbundles. 
If the version of Unity is 5.3.x, it provides *[ChunkBasedCompression](http://docs.unity3d.com/ScriptReference/BuildAssetBundleOptions.ChunkBasedCompression.html)* option which is newly added on Unity 5.3.x.


Usage
-----

* Select 'Tools/AssetBundles/Create Build Setting' menu item.
* Select the created *'AssetBundleBuildSetting.asset'* setting file which is found under the *'Assets/AssetBundleManager/Editor'* directory.
* Set assetbundle options.
* Set output directory where the created bundles are placed.
* Build it!


Known Issues
------------
* Highly recommended to use on Unity 5.x. (may work on Unity 4.x but not recommended)
* It does not contain any script to load assetbundles, See [an asset bundle demo for Unity5 on bitbucket site](https://bitbucket.org/Unity-Technologies/assetbundledemo) or other stuff for that.
* Duplicate a setting asset file if you need multiple settings for each of target platform.


Additional Notes
----------------

It is also available to mark an asset as an assetbunle by setting its name with *[AssetImporter.assetBundleName](http://docs.unity3d.com/ScriptReference/AssetImporter-assetBundleName.html)*.
Consider it to use spreadsheet or xml file for those configuration to do it as batching job instead of doing tedious thing like mouse click on every assets which are needed to be assetbundles.

The following codesnip shows to do that: 

```csharp
    [MenuItem("Tools/AssetBundles/Set AssetBundles from Spreadsheet", false, 0)]
    static void SetAssetBundlesFromSpreadsheet()
    {
        // Assume that each cells of the worksheet has path of an asset and a name of assetbundle
        foreach (string path in sheet.cells)
        {
            ...
            // Retrieve assetimporter for the given path
            AssetImporter assetImporter = AssetImporter.GetAtPath(path);

            // Specify bundle name for the asset.
            assetImporter.assetBundleName = asset.name;

            // Save the changes.
            assetImporter.SaveAndReimport();
            ...
        }
    }

```

See [Unity-QuickSheet](https://github.com/kimsama/Unity-QuickSheet) to get spreadsheet work with Unity.


References
----------
* [Official Unity3D document page for AssetBundle5x](http://docs.unity3d.com/500/Documentation/Manual/BuildingAssetBundles5x.html)
* [Official Unity3D tutorial page for Assetbundles and the Assetbundle Manager](https://unity3d.com/kr/learn/tutorials/topics/scripting/assetbundles-and-assetbundle-manager)
* [An asset bundle demo for Unity5 on bitbucket site](https://bitbucket.org/Unity-Technologies/assetbundledemo)
* [A comprehensive document on the changes of assetbundle on Unity 5.3.x at the blog page of テラシュールブログ](http://tsubakit1.hateblo.jp/entry/2015/12/16/233336)
* [Official Unity3D document page for Asset Bundle Compression](http://docs.unity3d.com/Manual/AssetBundleCompression.html)
* [LZ4 compression related - AssetBundleのパフォーマンスを計測したかった～Unity5.3.1編～](http://veniegames.com/?p=262)
* [LZ4 compression related - Unity5.3のAssetBundleパフォーマンス計測](https://www.google.co.kr/url?sa=t&rct=j&q=&esrc=s&source=web&cd=6&cad=rja&uact=8&ved=0ahUKEwjllNOiwqbLAhVBpJQKHQN7DHUQFghJMAU&url=http%3A%2F%2Fqiita.com%2Fvui%2Fitems%2Fe25dacb22c085606e15f&usg=AFQjCNGYACO0hGvksrgCrjs_eecA6Aa5wA&sig2=-8DI6h-Rs8itXw85xmEVkQ&bvm=bv.115339255,d.dGo)
* [Improved Unity asset bundle file compression on Rich Geldreich's Tech Blog](http://richg42.blogspot.kr/2015/01/improved-unity-asset-bundle-file.html)


Other Stuffs
------------

* [AssetGraph](https://github.com/unity3d-jp/AssetGraph) - A visual toolset lets you configure and create Unity's AssetBundles. A toolset which uses different approach on handling AssetBundles.
* [BundleVersionChecker](https://github.com/kayy/BundleVersionChecker) - Smart workaround to get Unity's bundle version information from PlayerSettings.bundleVersion in your source code by automatic code generation from a Unity editor class.

License
-------

This code is distributed under the terms and conditions of the MIT license.

* *'SerializableDictionary'* code is borrowed from @vexe at [here](http://forum.unity3d.com/threads/finally-a-serializable-dictionary-for-unity-extracted-from-system-collections-generic.335797/).
* *'Utility.cs' code is borrowed from [an asset bundle demo](https://bitbucket.org/Unity-Technologies/assetbundledemo). 

The license of the that follow those.

Copyright (c) 2016 Kim, Hyoun Woo
