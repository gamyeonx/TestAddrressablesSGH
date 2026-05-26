
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RemoteCanvasUpdater : MonoBehaviour
{
    public GameObject CanvasReal;
    public AssetReferenceGameObject Canvas;

    public void OnClickButton()
    {
        Destroy(CanvasReal);
        Canvas.InstantiateAsync();
    }
}