using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actual player representation on the board.
/// </summary>
public class PlayerBoardPiece : BoardPiece {

	public void MoveUp()
    {
        if(CheckValidMove(x-1,y-1))
        {

        }
        SetPosition(x - 1, y - 1);
    }

    private void CheckValidMove(int x, int y)
    {
        return
    }

}
