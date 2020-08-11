using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private int sceneID;
    private AsyncOperation theLoadingScene;
    public void GoToScene(int scene){
        sceneID = scene;
        theLoadingScene = SceneManager.LoadSceneAsync(1);
        StartCoroutine("LoadSceneAsync");
    }
    IEnumerator LoadSceneAsync(){
        yield return new WaitUntil(() => theLoadingScene.isDone);
        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(sceneID);
        Image progressbar = GameObject.Find("ProgressBar").GetComponent<Image>();

        while (loadingLevel.progress < 1){
            progressbar.fillAmount = loadingLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
