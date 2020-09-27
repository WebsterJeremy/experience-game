using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable 649

public class GameController : MonoBehaviour
{
    #region AccessVariables

    public enum GameState { PAUSED, PLAYING };
    public static GameState GAME_STATE = GameState.PLAYING;
    public Player playerPublic; // used as a public gameobject for Ammo text display. Could merge with playerPrivate
    public Enemy enemyPublic; // used as a public gameobject for Ememy Health text display.
    public Text ammoText; // public element linked to UI Text cavas
    public Text enemyHealth; // public element linked to UI Text cavas


    [Header("Player")]
    [SerializeField] private GameObject player;

    #endregion
    #region PrivateVariables

    private Texture2D screenshot;

    private Dictionary<string, string> stats = new Dictionary<string, string>();

    #endregion
    #region Initlization

    private static GameController instance;
    public static GameController Instance // Assign Singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("GameController");
                    instance = instanceContainer.AddComponent<GameController>();
                }
            }
            return instance;
        }
    }

    IEnumerator Start()
    {
        Input.simulateMouseWithTouches = true;

        yield return new WaitForFixedUpdate();

//        money = GetStat("MONEY", 0);

        EffectController.TweenFadeScene(1f, 0f, 2f, () => {}); // Fade in from White on start.

        yield return new WaitForSeconds(0.3f);

//        SpawnPlayer();
    }

    void Update(){
        ammoText.text = "Ammo: " + playerPublic.Ammo;
        enemyHealth.text = "Enemy Player Damage: " + enemyPublic.EnemyHealth;
    }

    private void OnApplicationQuit()
    {
        foreach (string key in stats.Keys)
        {
            PlayerPrefs.SetString(key, stats[key]);
        }
    }


    #endregion
    #region Getters & Setters

    public GameObject Player { get { return player; } }

    public string GetStat(string key, string _default)
    {
        if (!stats.ContainsKey(key))
            SetStat(key, PlayerPrefs.GetString(key, _default));

        return stats.ContainsKey(key) ? stats[key] : _default;
    }
    public int GetStat(string key, int _default) { return int.Parse(GetStat(key, _default.ToString())); }

    public void SetStat(string key, string stat)
    {
        if (!stats.ContainsKey(key))
            stats.Add(key, stat);

        stats[key] = stat;
    }

    public static bool IsPlaying()
    {
        return GAME_STATE == GameState.PLAYING;
    }

    #endregion
    #region Main

    public void StartGame() { StartCoroutine(_StartGame()); }
    IEnumerator _StartGame()
    {
        yield return new WaitForSeconds(0.3f);

        GAME_STATE = GameState.PLAYING;
    }

    #endregion
}
