using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMainScene : MonoBehaviour
{
    public void OnClickNextMainScene()
    {
        SceneManager.LoadScene("MainScene 1");
    }    
}
