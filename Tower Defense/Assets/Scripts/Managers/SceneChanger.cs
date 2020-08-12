using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private int sceneID;
    private AsyncOperation theLoadingScene;
    private static SceneChanger instance;
    public static void GoToScene_Static(int scene){
        instance.GoToScene(scene);
    }
    private void Awake() {
        if (instance){
            Destroy(gameObject);
            return;
        }
        instance = this;
        //sceneID = SceneManager.GetActiveScene().buildIndex;
    }
    public void GoToScene(int scene){
        //SceneManager.UnloadSceneAsync(sceneID);
        sceneID = scene;
        theLoadingScene = SceneManager.LoadSceneAsync(1);
        StartCoroutine("LoadSceneAsync");
    }
    IEnumerator LoadSceneAsync(){
        yield return new WaitUntil(() => theLoadingScene.isDone);
        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
        Image progressbar = GameObject.Find("ProgressBar").GetComponent<Image>();

        while (loadingLevel.progress < 1){
            progressbar.fillAmount = loadingLevel.progress;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.UnloadSceneAsync(1);
    }
}
