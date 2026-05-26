using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject charecterObj;
    [SerializeField]
    private AssetReferenceGameObject[] buildingObjs;
    [SerializeField]
    private AssetReferenceT<AudioClip> soundBGM;
    [SerializeField]
    private AssetReferenceSprite FlagSprite;

    [SerializeField]
    private GameObject BGMObj;
    [SerializeField]
    private Image FlagImage;

    private List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        Button_SpawnObject();
    }


    public  void Button_SpawnObject()
    {
        charecterObj.InstantiateAsync().Completed += (obj) =>
        {
            gameObjects.Add(obj.Result);
        };
    }
}
