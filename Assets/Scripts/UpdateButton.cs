using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UpdateButton : MonoBehaviour
{
    private void Start()
    {
    }

    public void OnClick()
    {
        StartCoroutine(DownloadStart());
    }

    private IEnumerator DownloadStart()
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync("ButtonUi");

        while (!handle.IsDone)
        {
            Debug.Log(handle.PercentComplete);
            yield return null;
        }

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("다운로드 완료");
        }

        Addressables.Release(handle);
    }
}
