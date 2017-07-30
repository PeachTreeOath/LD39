using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitSwapper : TurnBehaviour {

    private SpriteRenderer spr;
    private Sprite nextSprite;

    private Renderer rend;
    private bool startTransition = false;
    private bool transitionRunning = false;
    private float endLerpTime = 0;

    void Start() {
        spr = GetComponent<SpriteRenderer>();
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetFloat("_GameTimeOffset", Time.time);
        rend.material.SetFloat("_StartTime", Time.time + 120); //don't start yet
        if (rend == null) {
            Debug.LogError("Couldn't load sprite renderer - Lerp won't work");
        }
    }

    void Update() {

        if (transitionRunning) {
            if (Time.time > endLerpTime) {
                //lerp complete, swap in new sprite
                Debug.Log("Lerp done");
                spr.sprite = nextSprite;
                transitionRunning = false;
                rend.material.SetFloat("_StartTime", Time.time + 120);
            }
        } else {
            //keep pushing back start time so the transition doesn't start.
            //note we could also just swap in a different shader until we want to lerp.
            rend.material.SetFloat("_StartTime", Time.time + 120);
        }
    }

    private void setNextSprite(Sprite nextSprite) {

        if (nextSprite != this.nextSprite && nextSprite != this.spr.sprite) {
            Debug.Log("Starting lerp");
            if (transitionRunning && this.nextSprite != null) {
                //premature abort for in progress transitions
                spr.sprite = this.nextSprite;
                rend.material.SetTexture("_EndTex", null);
            }
            if (rend.material.GetTexture("_MainTex") == null) {
                rend.material.SetTexture("_MainTex", spr.sprite.texture);
            }
            this.nextSprite = nextSprite;
            rend.material.SetFloat("_StartTime", Time.time);
            rend.material.SetTexture("_EndTex", nextSprite.texture);
            endLerpTime = Time.time + rend.material.GetFloat("_Duration");
            transitionRunning = true;
        }

    }

    public override void OnAdvanceTurn() {
        //Debug.Log("Age is " + LifeStatManager.instance.age);
        if (LifeStatManager.instance.age >= 90) {
            setNextSprite(ResourceLoader.instance.portraitChip90);
        } else if (LifeStatManager.instance.age >= 80) {
            setNextSprite(ResourceLoader.instance.portraitChip80);
        } else if (LifeStatManager.instance.age >= 70) {
            setNextSprite(ResourceLoader.instance.portraitChip70);
        } else if (LifeStatManager.instance.age >= 60) {
            setNextSprite(ResourceLoader.instance.portraitChip60);
        } else if (LifeStatManager.instance.age >= 50) {
            setNextSprite(ResourceLoader.instance.portraitChip50);
        } else if (LifeStatManager.instance.age >= 40) {
            setNextSprite(ResourceLoader.instance.portraitChip40);
        } else if (LifeStatManager.instance.age >= 30) {
            setNextSprite(ResourceLoader.instance.portraitChip30);
        } else if (LifeStatManager.instance.age >= 20) {
            setNextSprite(ResourceLoader.instance.portraitChip20);
        } else if (LifeStatManager.instance.age >= 10) {
            setNextSprite(ResourceLoader.instance.portraitChip10);
        }
    }

}
