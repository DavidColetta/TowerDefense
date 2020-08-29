using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider panSensitivitySlider;
    public AudioMixer mixer;
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    public TMP_Dropdown difficultyDropdown;
    public CameraMovement cameraMovement;
    private bool ignoreFirstResolutionChange = true;
    private void Start() {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (panSensitivitySlider){
            panSensitivitySlider.value = PlayerPrefs.GetInt("panSensitivity", 15);
        }
        if (cameraMovement){
            cameraMovement.panSpeed = PlayerPrefs.GetInt("panSensitivity", 15);
        }
        if (difficultyDropdown){
            difficultyDropdown.value = PlayerPrefs.GetInt("difficultyLevel", 1);
        }

        if (MasterVolumeSlider){
            MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        }
        if (MusicVolumeSlider){
            MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }
        if (SFXVolumeSlider){
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
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
        if (ignoreFirstResolutionChange){
            ignoreFirstResolutionChange = false;
            return;
        }
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
    public void SetPanSensitivity(float sensitivity){
        PlayerPrefs.SetInt("panSensitivity", Mathf.RoundToInt(sensitivity));
        if (cameraMovement != null){
            cameraMovement.panSpeed = Mathf.RoundToInt(sensitivity);
        }
    }
    public void SetMasterVolume(float volume){
        SetVolume(volume, "MasterVolume");
    }
    public void SetMusicVolume(float volume){
        SetVolume(volume, "MusicVolume");
    }
    public void SetSFXVolume(float volume){
        SetVolume(volume, "SFXVolume");
    }
    public void SetVolume(float volume, string name = "MasterVolume"){
        PlayerPrefs.SetFloat(name,volume);
        mixer.SetFloat(name, Mathf.Log10(volume)*20);
    }
    public void SetDifficultyLevel(int _difficultyLevel){
        PlayerPrefs.SetInt("difficultyLevel", _difficultyLevel);
        DifficultyManager.difficultyLevel = _difficultyLevel;
    }
    public void QuitApplication(){
        Application.Quit();
    }
}
