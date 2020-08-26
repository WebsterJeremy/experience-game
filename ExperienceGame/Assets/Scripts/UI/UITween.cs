using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class UITween : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    [Header("Pressed")]
    [SerializeField] private bool enablePressScale = true;
    [SerializeField] private float pressedDuration = 0.15f;

    [Header("Hover")]
    [SerializeField] private bool enableHover = false;
    [SerializeField] private float hoverHeight = 15f;
    [SerializeField] private float hoverSpeed = 0.4f;

    [Header("Spin")]
    [SerializeField] private bool enableSpin = false;
    [SerializeField] private float spinSpeed = 2f;

    [Header("Shake")]
    [SerializeField] private bool enableShake = false;
    [SerializeField] private float shakeSpeed = 2f;
    [SerializeField] private float shakeWait = 1f;
    [SerializeField] private Vector2 shakeBetweenX = new Vector2(0, 0);
    [SerializeField] private Vector2 shakeBetweenY = new Vector2(0, 0);
    [SerializeField] private Vector2 shakeBetweenZ = new Vector2(-30, 30);

    [Header("Scale")]
    [SerializeField] private bool enableScale = false;
    [SerializeField] private float scaleSpeed = 0.4f;
    [SerializeField] private Vector2 scaleSize = new Vector2(0, 0.2f);

    [Header("Animate")]
    [SerializeField] private bool enableAnimate = false;
    [SerializeField] private float animateFPS = 10f;
    [SerializeField] private Sprite[] animateFrames;

    [Header("Fade")]
    [SerializeField] private bool enableFade = false;
    [SerializeField] private float fadeSpeed = 0.4f;
    [SerializeField] private Vector2 fadeBetween = new Vector2(0.2f, 0.8f);

    private RectTransform rect;
    private Button btn;
    private Vector2 initPos;
    private Image image;
    private TextMeshProUGUI ui;

    private float hoverGoal = 0f;
    private bool hoverUp = false;

    private Vector2 scaleGoal;
    private bool scaleUp = false;

    private float fadeGoal;
    private Color fadeColor;
    private bool fadeUp = false;

    private float shakeLast;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        if (enablePressScale) btn = GetComponent<Button>();
        image = GetComponent<Image>();
        ui = GetComponent<TextMeshProUGUI>();

        initPos = rect.anchoredPosition;
        hoverGoal = rect.anchoredPosition.y + (hoverHeight / 2);

        scaleGoal = new Vector2(1 + scaleSize.x, 1 + scaleSize.y);

        fadeGoal = fadeBetween.x;
        if (enableFade)
            fadeColor = ui.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (enablePressScale) EffectController.TweenScale(rect, new Vector3(1.1f, 1.1f, 1.1f), pressedDuration, () => {});
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (enablePressScale) EffectController.TweenScale(rect, new Vector3(1f, 1f, 1f), pressedDuration, () => {});
    }

    void Hover()
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Mathf.Lerp(rect.anchoredPosition.y, hoverGoal, Time.deltaTime * hoverSpeed));

        if (Mathf.Abs(hoverGoal - rect.anchoredPosition.y) < 10f)
        {
            hoverUp = !hoverUp;
            hoverGoal = hoverUp ? initPos.y + (hoverHeight) : initPos.y - (hoverHeight);
        }
    }

    void Spin()
    {
        rect.localRotation = rect.localRotation * Quaternion.Euler(0, 0, spinSpeed);
    }

    void Scale()
    {
        if (rect.localScale.x > 0.01f && rect.localScale.y > 0.01f)
        {
            if (scaleUp)
                rect.localScale = new Vector2(rect.localScale.x + (scaleSize.x * scaleSpeed), rect.localScale.y + (scaleSize.y * scaleSpeed));
            else
                rect.localScale = new Vector2(rect.localScale.x - (scaleSize.x * scaleSpeed), rect.localScale.y - (scaleSize.y * scaleSpeed));
        }

        if ((scaleUp ? (scaleGoal.x - rect.localScale.x) : (rect.localScale.x - scaleGoal.x)) < 0.01f &&
            (scaleUp ? (scaleGoal.y - rect.localScale.y) : (rect.localScale.y - scaleGoal.y)) < 0.01f)
        {
            scaleUp = !scaleUp;
            scaleGoal = scaleUp ? new Vector2(1 + scaleSize.x, 1 + scaleSize.y) : new Vector2(1 - scaleSize.x, 1 - scaleSize.y);
        }
    }

    void Animate()
    {
        int index = (int)(Time.time * animateFPS);
        index = index % animateFrames.Length; image.sprite = animateFrames[index];
    }

    void Fade()
    {
        fadeColor.a += (fadeUp ? fadeSpeed : -fadeSpeed);

        if (ui != null)
            ui.color = fadeColor;
        else
            image.color = fadeColor;

        if (Mathf.Abs(fadeColor.a - fadeGoal) < 0.02f)
        {
            fadeUp = !fadeUp;
            fadeGoal = fadeUp ? fadeBetween.y : fadeBetween.x;
        }
    }

    // Shake Left, Shake Right, then back, then mini shake back to center
    void Shake()
    {
        shakeLast = 0f;

        EffectController.TweenRotation(transform, Quaternion.Euler(shakeBetweenX.x, shakeBetweenY.x, shakeBetweenZ.x), (shakeSpeed-0.2f) / 2, () =>
        {
            EffectController.TweenRotation(transform, Quaternion.Euler(shakeBetweenX.y, shakeBetweenY.y, shakeBetweenZ.y), (shakeSpeed-0.2f) / 2, () =>
            {
                EffectController.TweenRotation(transform, Quaternion.Euler(0, 0, -5f), 0.1f, () =>
                {
                    EffectController.TweenRotation(transform, Quaternion.Euler(0, 0, 0), 0.1f, () =>
                    {

                    });
                });
            });
        });
    }

    void Update()
    {
        if (btn != null && !btn.interactable) return;

        if (enableHover) Hover();
        if (enableSpin) Spin();
        if (enableScale) Scale();
        if (enableAnimate) Animate();
        if (enableFade) Fade();
        if (enableShake && shakeLast >= shakeSpeed + shakeWait) Shake();

        shakeLast += Time.deltaTime;
    }
}
