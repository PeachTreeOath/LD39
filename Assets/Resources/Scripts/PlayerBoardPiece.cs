﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actual player representation on the board.
/// </summary>
public class PlayerBoardPiece : BoardPiece
{

    public void MoveUp()
    {
        if (CheckValidMove(x, y + 1))
        {
            SetPosition(x, y + 1);
        }
    }
    public void MoveDown()
    {
        if (CheckValidMove(x, y - 1))
        {
            SetPosition(x, y - 1);
        }
    }
    public void MoveLeft()
    {
        if (CheckValidMove(x - 1, y))
        {
            SetPosition(x - 1, y);
        }
    }
    public void MoveRight()
    {
        if (CheckValidMove(x + 1, y))
        {
            SetPosition(x + 1, y);
        }
    }
    private bool CheckValidMove(int x, int y)
    {
        return board.IsSquareMovable(x, y);
    }

    public List<BoardPiece> GetPickups() {
        if (isOverPickup()){
            Debug.Log("Cash me outside");
            return getPickups();
        }
        return new List<BoardPiece>();
    }

    public override int GetContentType() {
        return BoardPiece.PLAYER;
    }

}
