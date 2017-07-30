using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaifuBoardPiece : BoardPiece {

    public int maxHP;
    public Trait trait; 

    private int hp;

    public override int GetContentType() {
        if (hp <= 0)
        {
            return BoardPiece.PICKUP;
        }
        else
        {
            bump();
            return BoardPiece.BARRIER;
        }
    }

    private void Start()
    {
        hp = maxHP;
    }

    private void bump()
    {
        if (hp > 0)
        {
            hp--;
        }
    }
        

}