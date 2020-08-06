using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class EffectController : MonoBehaviour
{
    #region AccessVariables


    [SerializeField] private Image fadeImage;
    [Serializable]
    public class Effect
    {
        public string key = "";
    }
    [SerializeField] private Effect[] particleEffects;


    #endregion
    #region PrivateVariables


    private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();


    #endregion
    #region Initlization


    private static EffectController instance;
    public static EffectController Instance // Assign Singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectController>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("EffectController");
                    instance = instanceContainer.AddComponent<EffectController>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        foreach (Effect effect in particleEffects)
        {
            effects.Add(effect.key, effect);
        }
    }


    #endregion
    #region Particales


    public static Effect GetEffect(string key)
    {
        return Instance.effects.ContainsKey(key) ? Instance.effects[key] : null;
    }

    public static GameObject PlayEffect(string key, Vector3 posistion)
    {
        Effect effect = GetEffect(key);

        if (effect != null)
        {
            return Instantiate(Resources.Load("Effects/" + effect.key), posistion, Quaternion.identity) as GameObject;
        }

        return null;
    }


    #endregion
    #region Tween


    public static void TweenScale(Transform rect, Vector3 endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenScale(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenScale( Transform rect, Vector3 endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.localScale = Vector3.Lerp(rect.localScale, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenPosition(Transform rect, Vector3 endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenPosition(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenPosition(Transform rect, Vector3 endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.localPosition = Vector3.Lerp(rect.localPosition, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenPositionWorld(Transform rect, Vector3 endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenPositionWorld(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenPositionWorld(Transform rect, Vector3 endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.position = Vector3.Lerp(rect.position, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenRotation(Transform rect, Quaternion endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenRotation(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenRotation(Transform rect, Quaternion endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.localRotation = Quaternion.Lerp(rect.localRotation, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }


    //////////////


    public static void TweenAnchor(RectTransform rect, Vector2 endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenAnchor(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenAnchor(RectTransform rect, Vector2 endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenScaleBack(Transform rect, Vector3 endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenScaleBack(rect, endValue, duration, callback)); }
    private static IEnumerator _TweenScaleBack(Transform rect, Vector3 endValue, float duration, System.Action callback)
    {
        float animTime = 0f;
        Vector3 startValue = rect.localScale;
        duration = duration / 2;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.localScale = Vector3.Lerp(rect.localScale, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        animTime = 0f;

        while (animTime < duration)
        {
            if (rect == null) yield return null;
            rect.localScale = Vector3.Lerp(rect.localScale, startValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenRotateAround(Transform rect, Vector3 point, Vector3 axis, float angle, float duration, System.Action callback) { Instance.StartCoroutine(_TweenRotateAround(rect,point,axis,angle,duration,callback)); }
    private static IEnumerator _TweenRotateAround(Transform rect, Vector3 point, Vector3 axis, float angle, float duration, System.Action callback)
    {
        float step = 0;
        float rate = 1.0f / duration;
        float smoothStep = 0;
        float lastStep = 0;

        while (step < 1f)
        {
            step += Time.deltaTime * rate;
            smoothStep = Mathf.SmoothStep(0, 1, step);

            if (rect == null) yield return null;
            rect.RotateAround(point, axis, angle * (smoothStep - lastStep));

            yield return new WaitForEndOfFrame();

            lastStep = smoothStep;
        }

        if (step > 1.0) rect.RotateAround(point, axis, angle * (1f - lastStep));

        callback();
    }

    #endregion
    #region Fade


    public static void TweenFade(Image img, float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFade(img, startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFade(Image img, float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        img.color = new Color(1, 1, 1, startValue);

        while (animTime < duration)
        {
            img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, endValue, animTime / duration));

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        callback();
    }

    public static void TweenFadeScene(float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFadeScene(startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFadeScene(float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;
        Image fadeImage = Instance.fadeImage;

        if (!fadeImage.gameObject.activeSelf)
        {
            fadeImage.gameObject.SetActive(true);
        }

        fadeImage.color = new Color(1, 1, 1, startValue);

        while (animTime < duration)
        {
            fadeImage.color = new Color(1, 1, 1, Mathf.Lerp(fadeImage.color.a, endValue, animTime / duration));

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }

        if (endValue < 0.1f)
        {
            fadeImage.gameObject.SetActive(false);
        }

        callback();
    }

    public static void TweenFade(Material material, float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFadeMat(material, startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFadeMat(Material material, float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        material.color = new Color(material.color.r, material.color.g, material.color.b, startValue);

        while (animTime < duration)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(material.color.a, endValue, animTime / duration));

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
        callback();
    }

    public static void TweenFade(SpriteRenderer spriteRenderer, float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFadeSprite(spriteRenderer, startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFadeSprite(SpriteRenderer spriteRenderer, float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, startValue);

        while (animTime < duration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, endValue, animTime / duration));

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
        callback();
    }

    public static void TweenFade(TMPro.TextMeshProUGUI text, float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFadeText(text, startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFadeText(TMPro.TextMeshProUGUI text, float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        text.alpha = startValue;

        while (animTime < duration)
        {
            text.alpha = Mathf.Lerp(text.alpha, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
        callback();
    }

    public static void TweenFade(CanvasGroup canvas, float startValue, float endValue, float duration, System.Action callback) { Instance.StartCoroutine(_TweenFadeCanvas(canvas, startValue, endValue, duration, callback)); }
    private static IEnumerator _TweenFadeCanvas(CanvasGroup canvas, float startValue, float endValue, float duration, System.Action callback)
    {
        float animTime = 0f;

        canvas.alpha = startValue;

        while (animTime < duration)
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, endValue, animTime / duration);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
        callback();
    }


    #endregion
    #region Screenshot


    public static void TakeScreenshot(System.Action<Texture2D> callback) { Instance.StartCoroutine(_TakeScreenshot(callback)); }
    private static IEnumerator _TakeScreenshot(System.Action<Texture2D> callback)
    {
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height);

        yield return new WaitForEndOfFrame();
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        yield return new WaitForSeconds(0.5f);

        callback(screenshot);
    }


    #endregion
}
