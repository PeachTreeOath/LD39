using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBoardPiece : BoardPiece
{

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        FogBoardPieceListener listener = gameObject.AddComponent<FogBoardPieceListener>();
        listener.piece = this;
    }

    public void CheckDestroy()
    {
        PlayerBoardPiece player = board.GetPlayerBoardPiece();

        if (player != null)
        {
            /* Ressurect this if we want to have true fog of war reveal rather than vision
            if (Mathf.Abs(player.x - x) < 3)
            {
                Destroy(gameObject);
            }
            else if (Mathf.Abs(player.y - y) < 3)
            {
                Destroy(gameObject);
            }
            */
            if (Mathf.Abs(player.x - x) < 3)
            {
                spr.enabled = false;
            }
            else
            {
                spr.enabled = true;
            }
            if (Mathf.Abs(player.y - y) < 3)
            {
                spr.enabled = false;
            }
            else
            {
                spr.enabled = true;
            }
        }
        else
        {
            Debug.LogError("No player found for some reason...");
        }
    }

    public override int GetContentType()
    {
        return BoardPiece.FOG;
    }
}

public class FogBoardPieceListener : TurnBehaviour
{
    public FogBoardPiece piece;

    public override void OnAdvanceTurn()
    {
        piece.CheckDestroy();
    }
}