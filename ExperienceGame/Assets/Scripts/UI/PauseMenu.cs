using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private RectTransform rectPauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // set the button text for switching between full screen and windowed
        string buttonText = Screen.fullScreen ? "SET WINDOWED" : "SET FULL SCREEN";
        GameObject.Find("btn_fullscreen").GetComponentInChildren<TMP_Text>().text = buttonText;

        // the drop down for changing resolutions
        dropDown = GameObject.Find("dropdown_resolution").GetComponent<TMP_Dropdown>();

        // set the inital text of our dropdown
        dropDown.GetComponentInChildren<TMP_Text>().text = "SET RESOLUTION";
    }

    public void Open()
    {
        // reset the text of the drop down
        dropDown.GetComponentInChildren<TMP_Text>().text = "SET RESOLUTION";

        rectPauseMenu.gameObject.SetActive(true);
        EffectController.TweenFade(rectPauseMenu.GetComponent<CanvasGroup>(), 0f, 1f, fadeSpeed, () => {});

        Time.timeScale = 0;
        GameController.GAME_STATE = GameController.GameState.PAUSED;
    }

    public void Resume()
    {
        rectPauseMenu.gameObject.SetActive(false);
        EffectController.TweenFade(rectPauseMenu.GetComponent<CanvasGroup>(), 1f, 0f, fadeSpeed, () => {});

        Time.timeScale = 1;
        GameController.GAME_STATE = GameController.GameState.PLAYING;
    }

    public void ExitToMainMenu()
    {
        // load main menu scnee
        SceneManager.LoadScene(0);
    }

  // Set the resolution of the game
  // based on the drop down value the user selects

  // index is the index of the value that was selected
  // 0 is the first drop down value
  // 1 is the second drop down value, etc
    public void SetResolution(int index)
    {
        // the new width and height of the resolution to set
        int newWidth = 0;
        int newHeight = 0;

        // index is the index of the dropdown value
        // see: dropdown_resolution in PauseMenu
        switch (index)
        {
            case 0:
                newWidth = 1280;
                newHeight = 720;
                break;
            case 1:
                newWidth = 1920;
                newHeight = 1080;
                break;
            case 2:
                newWidth = 2560;
                newHeight = 1440;
                break;
            case 3:
                newWidth = 3840;
                newHeight = 2160;
                break;
        }

        // reset the text of the drop down
        dropDown.GetComponentInChildren<TMP_Text>().text = "SET RESOLUTION";

        // the HUD will need to be repositioned on resolution changes
        GameController.HUD.SetHUDPosition(newWidth, newHeight);

        // change the resolution
        Screen.SetResolution(newWidth, newHeight, Screen.fullScreen);
    }

  // Toggle the game between full screen and windowed
    public void ToggleFullScreen()
    {
        // change the button text
        string buttonText = Screen.fullScreen ? "SET FULL SCREEN" : "SET WINDOWED";
        GameObject.Find("btn_fullscreen").GetComponentInChildren<TMP_Text>().text = buttonText;

        // toggle full screen
        Screen.fullScreen = !Screen.fullScreen;
    }
}
