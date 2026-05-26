using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class DownManagerR : MonoBehaviour
{

    public void OnClickUpdateMainScene()
    {
        StartCoroutine(LoadProcess());
    }
    IEnumerator LoadProcess()
    {
        AsyncOperationHandle initHandle = Addressables.InitializeAsync();
        yield return initHandle;

        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("ButtonUi");

        while (!downloadHandle.IsDone)
        {
            Debug.Log(downloadHandle.PercentComplete);
            yield return null;
        }

        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("다운로드 완료");
        }

        Addressables.Release(downloadHandle);

        //SceneManager.LoadScene("MainScene");

        Addressables.LoadSceneAsync("MainScene 1");
    }
}
