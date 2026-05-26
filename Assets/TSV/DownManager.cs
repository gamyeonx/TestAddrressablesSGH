using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DownManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject waitMessage;
    public GameObject downMessage;
    public Slider douwSlider;
    public TextMeshProUGUI sizeInfoText;
    public TextMeshProUGUI downValText;

    [Header("Label")]
    public AssetLabelReference defaultLabel;
    public AssetLabelReference matLabel;

    private long patchSize;
    private Dictionary<string, long> patchMap = new Dictionary<string, long>();

    private void Start()
    {
        waitMessage.SetActive(true);
        downMessage.SetActive(false);
        StartCoroutine(InitAddressable());
        //StartCoroutine(CheckUpdateFiles());
    }

    IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
    }

    IEnumerator CheckUpdateFiles()
    {
        var labels = new List<string>() { defaultLabel.labelString, matLabel.labelString };

        patchSize = default;

        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            patchSize += handle.Result;
        }

        if (patchSize > decimal.Zero)
        {
            waitMessage.SetActive(false);
            downMessage.SetActive(true);

            sizeInfoText.text = GetFileSize(patchSize);
        }
        else
        {
            downValText.text = "100%";
            douwSlider.value = 1f;
            yield return new WaitForSeconds(2f);
            LoadingManager.LoadScene("Main");

        }

    }

    private string GetFileSize(long byteCnt)
    {
        string size = "0 Bytes";

        if (byteCnt >= 1073741824.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1073741824.0) + " GB";
        }
        else if (byteCnt >= 1048576.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1048576.0) + " MB";

        }
        else if (byteCnt >= 1024.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1024.0) + " kB";
        }
        else if (byteCnt > 0 && byteCnt < 1024.0)
        {
            size = byteCnt.ToString() + "Bytes";
        }



        return size;
    }

    public void Button_DownLoad()
    {
        StartCoroutine(PatchFiles());
    }

    IEnumerator PatchFiles()
    {
        var labels = new List<string>() { defaultLabel.labelString, matLabel.labelString };

        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            if (handle.Result != decimal.Zero)
            {
                StartCoroutine(DownLoadLabel(label));
            }
        }

        yield return CheckDownLoad();
    }

    IEnumerator DownLoadLabel(string label)
    {
        patchMap.Add(label, 0);

        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(label, false);

       while (!handle.IsDone)
        {
            patchMap[label] = handle.GetDownloadStatus().DownloadedBytes;
            yield return new WaitForEndOfFrame();
        }

        patchMap[label] = handle.GetDownloadStatus().TotalBytes;
        Addressables.Release(handle);
    }

    IEnumerator CheckDownLoad()
    {
        var total = 0f;
        downValText.text = "0%";

        while (true)
        {
            total += patchMap.Sum(tmp => tmp.Value);

                douwSlider.value = total / patchSize;
            downValText.text = (int)((total / patchSize) * 100) + "%";

            if (total == patchSize)
            {
                LoadingManager.LoadScene("Main");
                break;
            }

            total = 0f;
            yield return new WaitForEndOfFrame();
        }
    }
}
