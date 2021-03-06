﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager> {

    public float secondsToFade;
    public float secondsToRevealHorizontalLine;
    public float secondsToRevealVerticalLine;
    public float secondsToPauseBeforeSceneTransition;
    public float secondsToPauseOnDeath;

    private bool endingStarted = false;
    private bool deathStarted = false;
    private float endingTime;
    private bool horizontalWipeDone = false;
    private bool verticalWipeDone = false;
    private bool finalPauseDone = false;
    private bool coroutineInProgress = false;

    SpriteRenderer blackLineHorizontal;
    SpriteRenderer blackLineVertical;
    SpriteRenderer horizontalLineMask;
    SpriteRenderer verticalLineMask;
    SpriteRenderer whiteMask;
    SpriteRenderer gameOverText;

    PortraitSwapper portraitSwapper;

    public void SkipTutorial(int levelToStartNext) {
        LogManager.instance.Log("So we have an expert here - Skipping Tutorial...");
        PermanentStatManager.instance.generation = levelToStartNext; //I think this is unused
        PermanentStatManager.instance.currentLevel = levelToStartNext;
        playEndingEffects();
    }

    public void TransitionToNextLevel()
    {
        PermanentStatManager.instance.generation++;
        PermanentStatManager.instance.currentLevel++;
        playEndingEffects();
    }

    public void ReplayCurrentLevel()
    {
        playDeathEffects();
    }

    private void playEndingEffects()
    {
        AudioManager.instance.PlayMusicOnce("VictoryTheme", AudioManager.instance.GetMusicVolume());
        endingTime = 0f;
        endingStarted = true;

        

        //kicks off these coroutines
        FadeUI.instance.FadeMe();
        FadeSprite.instance.FadeMe();

        PlayerController.instance.inputEnabled = false;
        
    }
  
    private void playDeathEffects()
    {
        AudioManager.instance.PlayMusicOnce("LoseTheme", AudioManager.instance.GetMusicVolume());
        endingTime = 0f;
        deathStarted = true;
        gameOverText.enabled = true;

        //kicks off these coroutines
        Color color = Color.black;
        color.a = 0;
        whiteMask.color = color;

        FadeUI.instance.FadeMe();
        FadeSprite.instance.FadeMe();

        PlayerController.instance.inputEnabled = false;
        PlayerController.instance.onDeathScreen = true;
        portraitSwapper.transitionToSkeleton();
    }
    IEnumerator DoHorizontalWipe()
    {
        blackLineHorizontal.enabled = true;
        blackLineVertical.enabled = true;
        horizontalLineMask.enabled = true;
        verticalLineMask.enabled = true;
        float horizontalWipeTime = 0f;

        Vector3 startingScale = horizontalLineMask.transform.localScale;
        float currentScaleX = startingScale.x;
        Debug.Log("startingScaleX = " + currentScaleX);
        while (currentScaleX > 0f)
        {
            horizontalWipeTime += Time.deltaTime;
            //Debug.Log("startingScale.x = " + startingScale.x);
            currentScaleX = Mathf.Max(startingScale.x - (startingScale.x * horizontalWipeTime) / secondsToRevealHorizontalLine, 0);
           // Debug.Log("currentScaleX = " + currentScaleX);
            Vector3 newScale = horizontalLineMask.transform.localScale;
            newScale.x = currentScaleX;
            horizontalLineMask.transform.localScale = newScale;
            yield return null;
        }
        horizontalWipeDone = true;
        coroutineInProgress = false;
        yield return null;
    }

    IEnumerator DoVerticalWipe()
    {
        float verticalWipeTime = 0f;
        float verticalWipeDistance = 8f; //needs to be adjusted to length of the line
        
        Vector3 startingPos = verticalLineMask.transform.localPosition;
        float currentPosY = startingPos.y;

        Vector3 cameraStartingPos = Camera.main.transform.localPosition;
        float cameraCurrentPosY = cameraStartingPos.y;

        while (Mathf.Abs(startingPos.y - currentPosY) < verticalWipeDistance)
        {
            verticalWipeTime += Time.deltaTime;
            //Debug.Log("startingPos.y = " + startingPos.y);
            currentPosY = startingPos.y - (verticalWipeDistance * verticalWipeTime) / secondsToRevealVerticalLine;
            //Debug.Log("currentPosY = " + currentPosY);
            cameraCurrentPosY = cameraStartingPos.y - (verticalWipeDistance * verticalWipeTime) / secondsToRevealVerticalLine;

            Vector3 newPos = verticalLineMask.transform.localPosition;
            Vector3 newCamPos = Camera.main.transform.localPosition;

            newPos.y = currentPosY;
            newCamPos.y = cameraCurrentPosY;

            verticalLineMask.transform.localPosition = newPos;
            Camera.main.transform.localPosition = newCamPos;
            yield return null;
        }
        verticalWipeDone = true;
        coroutineInProgress = false;
        yield return null;
    }

    void Start()
    {
        blackLineHorizontal = GameObject.Find("BlackLineHorizontal").GetComponent<SpriteRenderer>();
        blackLineVertical = GameObject.Find("BlackLineVertical").GetComponent<SpriteRenderer>();
        horizontalLineMask = GameObject.Find("HorizontalLineMask").GetComponent<SpriteRenderer>();
        verticalLineMask = GameObject.Find("VerticalLineMask").GetComponent<SpriteRenderer>();
        whiteMask = GameObject.Find("WhiteMask").GetComponent<SpriteRenderer>();
        portraitSwapper = GameObject.Find("Portrait").GetComponent<PortraitSwapper>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (endingStarted)
        {
            endingTime += Time.deltaTime;
            if (endingTime >= secondsToFade && !horizontalWipeDone && !coroutineInProgress)
            {
                endingTime = 0f;
                coroutineInProgress = true;
                //kickoff horizontalWipe
                StartCoroutine(DoHorizontalWipe());
                
            }
            else if (endingTime >= secondsToRevealHorizontalLine && !verticalWipeDone && !coroutineInProgress)
            {
                //kickoff verticalWipe
                endingTime = 0f;
                coroutineInProgress = true;
                StartCoroutine(DoVerticalWipe());
            }
            else if (endingTime >= secondsToRevealVerticalLine && !finalPauseDone && !coroutineInProgress)
            {
                //kickoff finalPause
                endingTime = 0f;
                finalPauseDone = true;
            }
            else if (endingTime >= secondsToPauseBeforeSceneTransition && !coroutineInProgress)
            {
                if(PermanentStatManager.instance.currentLevel == 13)
                    SceneManager.LoadScene("EndScreen");
                else
                    SceneManager.LoadScene("Game");
            }
        }

        if (deathStarted)
        {
            endingTime += Time.deltaTime;
            //Fade to black happens before this
            if (endingTime >= secondsToFade && !finalPauseDone && !coroutineInProgress)
            {
                //kickoff finalPause
                endingTime = 0f;
                finalPauseDone = true;
            }
            else if (endingTime >= secondsToPauseOnDeath && !coroutineInProgress)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

}
