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
        if (LifeStatManager.instance.age >= 90)
        {
            spr.sprite = ResourceLoader.instance.portraitChip90;
        }
        else if (LifeStatManager.instance.age >= 80)
        {
            spr.sprite = ResourceLoader.instance.portraitChip80;
        }
        else if (LifeStatManager.instance.age >= 70)
        {
            spr.sprite = ResourceLoader.instance.portraitChip70;
        }
        else if (LifeStatManager.instance.age >= 60)
        {
            spr.sprite = ResourceLoader.instance.portraitChip60;
        }
        else if (LifeStatManager.instance.age >= 50)
        {
            spr.sprite = ResourceLoader.instance.portraitChip50;
        }
        else if (LifeStatManager.instance.age >= 40)
        {
            spr.sprite = ResourceLoader.instance.portraitChip40;
        }
        else if (LifeStatManager.instance.age >= 30)
        {
            spr.sprite = ResourceLoader.instance.portraitChip30;
        }
        else if (LifeStatManager.instance.age >= 20)
        {
            spr.sprite = ResourceLoader.instance.portraitChip20;
        }
        else if (LifeStatManager.instance.age >= 10)
        {
            spr.sprite = ResourceLoader.instance.portraitChip10;
        }
    }

}
