using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager> {

    public float endingTotalTime;

    private bool endingStarted = false;
    private float endingTime;

    public void TransitionToNextLevel()
    {
        PermanentStatManager.instance.generation++;
        PermanentStatManager.instance.currentLevel++;
        playEndingEffects();
    }

    public void ReplayCurrentLevel()
    {
        SceneManager.LoadScene("Game");
    }

    private void playEndingEffects()
    {
        endingTime = 0f;
        endingStarted = true;

        FadeUI.instance.FadeMe();
        FadeSprite.instance.FadeMe();
        
    }

    void Update()
    {
        if (endingStarted)
        {
            endingTime += Time.deltaTime;
            if (endingTime >= endingTotalTime)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

}
