///////////////////////////////////////////////////////////////////////////////
///
/// ExternalProcessor.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using AssetBundles;

/// <summary>
/// Drop this into the object field on the BundleSetting.asset file.
/// 
/// </summary>
public class ExternalProcessor : ScriptableObject
{
    /// <summary>
    /// Whenever the BundleBuilder scriptableobject file is selected within editor, 
    /// ExternalProcossor is also instantiated if it is already dropped onto the BundleBuilder asset file. 
    /// </summary>
    void OnEnable()
    {
        // Specifies preprocessor and postprocessor for the delegates of the BundleBuilder.
        BundleBuilder.OnPreBuildProcessor = OnPreprocessor;
        BundleBuilder.OnPostBuildProcessor = OnPostprocessor;
    }

    public void OnPreprocessor(BundleBuilder builder)
    {
        // Do something before building assetbundlers here.
        // e.g. Do mark assets which are built as assetbundle etc.
        Debug.Log("OnPreprocessor.");
    }

    public void OnPostprocessor(BundleBuilder builder)
    {
        // Do something after building assetbundlers here.
        // e.g. Copy assetbundles under the StreamingAssets folder etc.
        Debug.Log("OnPostprocessor.");
    }
}
