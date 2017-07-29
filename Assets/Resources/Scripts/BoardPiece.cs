using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objects that are represented on a board.
/// </summary>
public class BoardPiece : MonoBehaviour {

    public int x; // 0-indexed, starting from left of board
    public int y; // 0-indexed, starting from bottom of board

    [HideInInspector]
    public Board board; // Board this piece belongs to

    public void SetPosition(int newX, int newY)
    {
        x = newX;
        y = newY;
        transform.localPosition = board.GetLocalPositionFromCoords(x, y);
    }

    public void SetBoard(Board newBoard)
    {
        board = newBoard;
        transform.SetParent(board.transform);
    }
}
