using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void GoToScene(int scene){
        SceneChanger.GoToScene_Static(scene);
    }
}
