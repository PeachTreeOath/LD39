using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour {

    public bool isFadingOut;
    public Texture2D fadeTexture;
    public float fadeSpeed = 5f;
    public int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    void oldFade()
    {
        if (isFadingOut)
        {
            alpha -= fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }

    public void FadeMe()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        while (color.a < 1)
        {
            Debug.Log("FadeOut: color.a = " + color.a);
            color.a += Time.deltaTime / 2;
            spriteRenderer.color = color;
            yield return null;
        }
        yield return null;
    }
}
