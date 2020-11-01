using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private int currentLevel = 0;

    public void ContinueGame()
    { 
        StartCoroutine(_ContinueGame()); 
    }
    IEnumerator _ContinueGame()
    {
        SceneManager.LoadScene(2); // Load Controller Scene

        //        if (currentFloor == floorCount) currentFloor = 0; // Check if level exists
        //        if (!dead) SetCurrentFloor(currentFloor + 1);

        yield return SceneManager.LoadSceneAsync(currentLevel + 3, LoadSceneMode.Additive); // Level is 0, but scene's in build start at 2 for levels
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevel + 3)); // This allows the Navmesh to work for the AI
        //        SpawnPlayer();
    }

    void Start()
    {
        // unload the pause menu scene if it's loaded
        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu"));
    }


    // Go to the Options menu
    public void Options() {
        SceneManager.LoadScene(1);
    }

    // Exit the Game
    // Doesn't work in the Unity Editor Game window,
    // only when you build and run the game.
    public void Quit() {
        Application.Quit();
    }
}
