using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitSwapper : TurnBehaviour
{

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public override void OnAdvanceTurn()
    {
        if (LifeStatManager.instance.age > 20)
        {
            spr.sprite = ResourceLoader.instance.portraitManSprite;
        }
        if (LifeStatManager.instance.age > 40)
        {
            spr.sprite = ResourceLoader.instance.portraitOldManSprite;
        }
    }

}
