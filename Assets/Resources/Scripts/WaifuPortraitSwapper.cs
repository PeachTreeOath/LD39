using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaifuPortraitSwapper : Singleton<WaifuPortraitSwapper>
{

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = ResourceLoader.instance.portraitFemaleASprite;
        spr.enabled = false;
    }

    public void TurnOnPortrait()
    {
        spr.enabled = true;
    }

}
