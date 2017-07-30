using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : Singleton<FadeUI> {
    public float secondsToFade;


    public void FadeMe()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / secondsToFade;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}
