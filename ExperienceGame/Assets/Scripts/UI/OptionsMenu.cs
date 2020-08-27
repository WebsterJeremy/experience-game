using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    // default width and height of the user's screen resolution
    int default_width;
    int default_height;

    public void Start() {
        default_width = Screen.currentResolution.width;
        default_height = Screen.currentResolution.height;

        // Debug.Log("Screen: " + default_width + " x " + default_height);
    }

    // Toggle the game between full screen and windowed
    public void ToggleFullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
        // Debug.Log(Screen.currentResolution);
    }

    // Set the resolution of the game
    // based on the drop down value the user selects

    // index is the index of the value that was selected
    // 0 is the first drop down value
    // 1 is the second drop down value, etc
    public void SetResolution(int index) {
        switch(index)
        {
            case 0:
                Screen.SetResolution(default_width, default_height, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
        }
    }

    // Go back to the Main Menu
    public void Back() {
        SceneManager.LoadScene(0);
    }
}
