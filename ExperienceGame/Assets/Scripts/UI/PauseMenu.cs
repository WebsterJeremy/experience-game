using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{

  TMP_Dropdown dropDown;

  // Start is called before the first frame update
  void Start()
  {
    // set the button text
    string buttonText = Screen.fullScreen ? "SET WINDOWED" : "SET FULL SCREEN";
    GameObject.Find("btn_fullscreen").GetComponentInChildren<TMP_Text>().text = buttonText;

    dropDown = GameObject.Find("dropdown_resolution").GetComponent<TMP_Dropdown>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Resume()
  {
    // resume the game time
    Time.timeScale = 1;

    // unload the pause menu
    SceneManager.UnloadSceneAsync(2);

    // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
  }

  public void ExitToMainMenu()
  {
    // load main menuy scnee
    SceneManager.LoadScene(0);
  }

  // Set the resolution of the game
  // based on the drop down value the user selects

  // index is the index of the value that was selected
  // 0 is the first drop down value
  // 1 is the second drop down value, etc
  public void SetResolution(int index)
  {

    switch (index)
    {
      case 0:
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        break;
      case 1:
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        break;
      case 2:
        Screen.SetResolution(2560, 1440, Screen.fullScreen);
        break;
      case 3:
        Screen.SetResolution(3840, 2160, Screen.fullScreen);
        break;
    }

    // GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "CHANGE RESOLUTION" });
  }

  public void RemoveDisplayOption()
  {
    // remove the "SET RESOLUTION" text option
    dropDown.options.RemoveAll(c => c.text == "SET RESOLUTION");
  }

  // Toggle the game between full screen and windowed
  public void ToggleFullScreen()
  {
    // change the button text
    string buttonText = Screen.fullScreen ? "SET FULL SCREEN" : "SET WINDOWED";
    GameObject.Find("btn_fullscreen").GetComponentInChildren<TMP_Text>().text = buttonText;

    Screen.fullScreen = !Screen.fullScreen;
  }
}
