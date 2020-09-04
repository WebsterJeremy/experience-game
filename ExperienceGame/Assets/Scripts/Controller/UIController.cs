using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class UIController : MonoBehaviour
{
    #region AccessVariables
    /*
    [Header("Mainmenu")]
    [SerializeField] private Button buttonPlay;

    [Header("Gameplay UI")]

    [SerializeField] private RectTransform rectMoney;

    [SerializeField] private TextMeshProUGUI textMoney;
    */
    [Header("Menus")]
    [SerializeField] private PauseMenu pauseMenu;


    #endregion
    #region PrivateVariables

    private enum MenuState { NONE, SETTINGS }
    private MenuState menuState = MenuState.NONE;
    private bool menuTransistion = false;

    #endregion
    #region Initlization

    private static UIController instance;
    public static UIController Instance // Assign Singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("UIController");
                    instance = instanceContainer.AddComponent<UIController>();
                }
            }
            return instance;
        }
    }

    IEnumerator Start()
    {
        AddButtonListeners();

        yield return new WaitForSeconds(0.1f);

        //        DisplayMoney(GameController.Instance.GetMoney());
    }

    #endregion
    #region Input


    void Update()
    {
        if (Input.GetKeyDown("escape") && GameController.IsPlaying())
        {
            pauseMenu.Open();
        }
    }


#endregion
#region Core


    void AddButtonListeners()
    {
        /*
        buttonPlay.onClick.AddListener(() =>
        {
//            SoundController.PlaySound("button");

            buttonPlay.interactable = false;
            HideMainMenu();
        });
        */
    }

    void HideMainMenu()
    {
        GameController.Instance.StartGame();
    }


    #endregion
    #region Gameplay UI

    /*
    public void ChangeMoney(int startValue, int endValue, float duration) { StartCoroutine(_ChangeMoney(startValue, endValue, duration)); }
    IEnumerator _ChangeMoney(int startValue, int endValue, float duration)
    {
        float animTime = 0f;
        float money = startValue;

        while (animTime < duration)
        {
            money = Mathf.Lerp(money, endValue, animTime / duration);
            DisplayMoney((int) money);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
    }

    private void DisplayMoney(int money)
    {
        textMoney.text = money.ToString();
        rectMoney.sizeDelta = new Vector2(100 + (textMoney.text.Length * 40), rectMoney.sizeDelta.y);
    }
    */
    #endregion
}
