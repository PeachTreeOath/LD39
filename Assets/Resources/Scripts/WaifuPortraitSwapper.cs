using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaifuPortraitSwapper : Singleton<WaifuPortraitSwapper>
{

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }

    public void TurnOnPortrait(WaifuBoardPiece piece)
    {
        //TODO: Check piece and tie it to the matching sprite
        spr.sprite = ResourceLoader.instance.portraitFemaleASprite;
        spr.enabled = true;
    }

}
