using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DelayMethod();

public class Tween : MonoBehaviour
{
    public static Tween instance = null;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator SetAlphaDontScale(SpriteRenderer spr, float startAlpha, float EndAlpha, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator MoveDontScale(GameObject obj, Vector3 StartPos, Vector3 EndPos, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.unscaledDeltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator Move(GameObject obj, Vector3 StartPos, Vector3 EndPos, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator Move(RectTransform obj, Vector3 StartPos, Vector3 EndPos, float time, float DelayMove = 0f)
    {
        float t = 0f;
        yield return new WaitForSeconds(DelayMove);

        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            obj.transform.localPosition = Vector3.Lerp(StartPos, EndPos, animationCurve.Evaluate(t));
            yield return null;
        }
        obj.transform.localPosition = EndPos;
    }

    public IEnumerator SetAlpha(SpriteRenderer spr, float startAlpha, float EndAlpha, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetAlpha(Image spr, float startAlpha, float EndAlpha, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetAlpha(Text spr, float startAlpha, float EndAlpha, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            spr.SetAlpha(Mathf.Lerp(startAlpha, EndAlpha, animationCurve.Evaluate(t)));
            yield return null;
        }
        spr.SetAlpha(EndAlpha);
    }

    public IEnumerator SetScale(RectTransform spr, Vector2 StartScale, Vector2 EndScale, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            spr.sizeDelta = Vector2.Lerp(StartScale, EndScale, animationCurve.Evaluate(t));
            yield return null;
        }
        spr.sizeDelta = EndScale;
    }

    public IEnumerator SetScale(Transform spr, Vector2 StartScale, Vector2 EndScale, float time)
    {
        float t = 0f;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        while (t < 1.0f)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / time);
            spr.localScale = Vector2.Lerp(StartScale, EndScale, animationCurve.Evaluate(t));
            yield return null;
        }
        spr.localScale = EndScale;
    }

    public IEnumerator DelayMethod(float time, DelayMethod delayMethod)
    {
        yield return new WaitForSeconds(time);

        delayMethod();
    }
}
