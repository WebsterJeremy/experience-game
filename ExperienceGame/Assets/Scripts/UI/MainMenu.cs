using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private int currentLevel = 0;

    public void ContinueGame() { StartCoroutine(_ContinueGame()); }
    IEnumerator _ContinueGame()
    {
        SceneManager.LoadScene(1); // Load Controller Scene

        //        if (currentFloor == floorCount) currentFloor = 0; // Check if level exists
        //        if (!dead) SetCurrentFloor(currentFloor + 1);

        yield return SceneManager.LoadSceneAsync(currentLevel + 2, LoadSceneMode.Additive); // Level is 0, but scene's in build start at 2 for levels

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevel + 2)); // This allows the Navmesh to work for the AI

        //        SpawnPlayer();
    }
}
