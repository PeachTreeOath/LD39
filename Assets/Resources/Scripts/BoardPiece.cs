using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objects that are represented on a board.
/// </summary>
public class BoardPiece : MonoBehaviour {

    public int x; // 0-indexed, starting from left of board
    public int y; // 0-indexed, starting from bottom of board

    private bool hasLastPos = false;
    private int lastXPos;
    private int lastYPos;

    [HideInInspector]
    public Board board; // Board this piece belongs to

    //piece types, used to identify board content pieces, 
    //MUST BE powers of 2 so we can mask the bits
    public static int OPEN_SPACE = 0;
    public static int PLAYER = 1;
    public static int BARRIER = 2;
    public static int PICKUP = 4;


    public void SetPosition(int newX, int newY)
    {
        //Don't forget to call SetBoard at least once before using this method!
        x = newX;
        y = newY;
        transform.localPosition = board.GetLocalPositionFromCoords(x, y);

        //update board tracking of this piece
        if (!hasLastPos) {
            board.UpdatePiece_Place(this, newX, newY);
        } else {
            board.UpdatePieceMoved(this, lastXPos, lastYPos, newX, newY);
        }
        lastXPos = x;
        lastYPos = y;
        hasLastPos = true;
    }

    public void SetBoard(Board newBoard)
    {
        board = newBoard;
        transform.SetParent(board.transform);
    }

    /// <summary>
    /// Returns true if this piece is currently positioned on the board over another thing
    /// </summary>
    /// <returns></returns>
    public bool isOverPickup() {
        return board.hasTileContent(x, y);
    }

    /// <summary>
    /// Override to provide the main type of content this piece contains.
    /// Default is none.
    /// </summary>
    /// <returns>Content constant from BoardPiece.cs</returns>
    public virtual int GetContentType() {
        return OPEN_SPACE;
    }

}
