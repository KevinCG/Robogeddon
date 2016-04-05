using UnityEditor;
using UnityEngine;
public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        
        BuildPipeline.BuildAssetBundles(Application.persistentDataPath);
    }
}