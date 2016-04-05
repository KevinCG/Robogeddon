using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class WWWNoCacheLoading: MonoBehaviour
{

    private string myBundleURL;// = "file://C:/Users/Kevin/Desktop/assetbundle2/Assets/AssetBundles/heyo";
    //private string myBundleURL; = Application.dataPath + "/AssetBundles/heyo";
    //private string myBundleURL = "https://drive.google.com/drive/folders/0B-fWRD1F6SxOZ2o1NDljbmVmT1k/heyo";
    public string assetName;
    //changed from private
    private GameObject myPrefabForLater;

    public static bool isDone;

    //ADDED
    MyClass myObject = new MyClass();

    IEnumerator Start()
    {
        //if(Application.isEditor)
        Debug.Log("file://" + Application.persistentDataPath + "/heyo");
        myBundleURL = "file://" + Application.persistentDataPath; // + "/heyo" to go to normal
        Debug.Log("json version" + JsonUtility.ToJson(myBundleURL));
        Debug.Log(Application.persistentDataPath);
        //  else
        //    myBundleURL = Application.dataPath + "/AssetBundles/heyo";

        //newewewewewewewew
        myObject.URL = myBundleURL;
        myObject.assetName = assetName;

        Debug.Log(myObject.URL + "   from object");
        Debug.Log(myObject.assetName+ "   from object");

        int rand = UnityEngine.Random.Range(0, 2);
        Debug.Log(rand);
        if (rand == 0)
        {
            string newURL = "{ \"URL\":\"" + myBundleURL + "/yyy\"}";
            string newAssetName = "{ \"assetName\":\"Sphere\"}";
            Debug.Log("THIS IS NEW URL " + newURL);
            Debug.Log("THIS IS NEW ASSET NAME " + newAssetName);
            JsonUtility.FromJsonOverwrite(newURL, myObject);
            JsonUtility.FromJsonOverwrite(newAssetName, myObject);
            Debug.Log("------AFTER CHANGES------");
            Debug.Log(myObject.URL);
            Debug.Log(myObject.assetName);
            Debug.Log(assetName);
        }
        else
        {
            string newURL = "{ \"URL\":\"" + myBundleURL + "/heyo\"}";
            string newAssetName = "{ \"assetName\":\"Cube\"}";
            Debug.Log("THIS IS NEW URL " + newURL);
            Debug.Log("THIS IS NEW ASSET NAME " + newAssetName);
            JsonUtility.FromJsonOverwrite(newURL, myObject);
            JsonUtility.FromJsonOverwrite(newAssetName, myObject);
            Debug.Log("------AFTER CHANGES------");
            Debug.Log(myObject.URL);
            Debug.Log(myObject.assetName);
            Debug.Log(assetName);
        }
        
       

        using (WWW www = new WWW(myObject.URL))
        {
            yield return www;
            if (www.error != null)
            {
                throw new Exception("WWW download had an error:" + www.error);
            }

            AssetBundle bundle = www.assetBundle;
            if (www.isDone)
            {
                isDone = true;
                Debug.Log("WWW is done");
            }

            if (assetName == "")
            {
                Instantiate(bundle.mainAsset);
            }
            else
            {
                GameObject newObj = Instantiate(bundle.LoadAsset(myObject.assetName)) as GameObject;
                myPrefabForLater = bundle.LoadAsset(myObject.assetName) as GameObject;
               // string json = JsonUtility.ToJson(myPrefabForLater.transform);
              //  Debug.Log(json);
            }

            

            bundle.Unload(false);
        }
    }

    public GameObject getPrefab()
    {
        return myPrefabForLater;
    }

}
