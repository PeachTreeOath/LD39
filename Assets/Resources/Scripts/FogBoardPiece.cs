using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBoardPiece : BoardPiece
{

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void CheckDestroy()
    {
        PlayerBoardPiece player = board.GetPlayerBoardPiece();

        /* Ressurect this if we want to have true fog of war reveal rather than vision
        if (Mathf.Abs(player.x - x) < 3)
            Destroy(gameObject);
        else if (Mathf.Abs(player.y - y) < 3)
            Destroy(gameObject);
        */
        if (Mathf.Abs(player.x - x) < 3)
            spr.enabled = false;
        else
            spr.enabled = true;

        if (Mathf.Abs(player.y - y) < 3)
            spr.enabled = false;
        else
            spr.enabled = true;
    }
    public override int GetContentType()
    {
        return BoardPiece.FOG;
    }
}
