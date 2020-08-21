using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider dragSensitivitySlider;
    public TMP_Dropdown difficultyDropdown;
    public static int dragSensitivity = 15;
    public CameraMovement cameraMovement;
    private void Start() {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (dragSensitivitySlider){
            dragSensitivitySlider.value = dragSensitivity;
        }
        if (cameraMovement){
            cameraMovement.panSpeed = dragSensitivity;
        }
        if (difficultyDropdown){
            difficultyDropdown.value = DifficultyManager.difficultyLevel-1;
        }

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width+" x "+resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
    public void SetDragSensitivity(float sensitivity){
        dragSensitivity = Mathf.RoundToInt(sensitivity);
        if (cameraMovement != null){
            cameraMovement.panSpeed = dragSensitivity;
        }
    }
    public void SetDifficultyLevel(int _difficultyLevel){
        DifficultyManager.difficultyLevel = _difficultyLevel + 1;
    }
    public void QuitApplication(){
        Application.Quit();
    }
}
