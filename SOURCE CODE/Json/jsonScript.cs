using UnityEngine;
using System.Collections;
using System;

public class jsonScript : MonoBehaviour
{
    //base bundle in string form
    private static string baseURL = "file://" + Application.persistentDataPath;

    //base bundle in json format
    private static string baseJsonURL = "{ \"URL\":\"" + baseURL;

    //base asset name in json format
    private static string baseJsonAssetName = "{ \"assetName\":";


    //takes an obj of type MyClass, a bundle, and an asset name
    //and changes the json of the obj, overwriting the asset name
    //and the url with a new bundle and asset name
    public static jsonClass overWriteJSON(jsonClass obj, string bundle, string assetName)
    {
        //jsonURL to use to overwrite current url in our json
        string jsonURL = bundle = baseJsonURL + "/" + bundle + "\"}";

        //jsonAssetName to use to overwrite current assetName in our json
        string jsonAssetName = baseJsonAssetName + "\"" + assetName + "\"}";

        //actually overwrite our json
        JsonUtility.FromJsonOverwrite(jsonURL, obj);
        JsonUtility.FromJsonOverwrite(jsonAssetName, obj);

        //return the obj with its json being overwritten
        return obj;
    }
}
