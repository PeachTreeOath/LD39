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

    private Vector2[] lerpFromTo = new Vector2[2];
    private float lerpEndTime = 0;
    private bool moving = false;

    private static float MOVE_DURATION = 0.10f; //in seconds

    //TODO use [Flags] enum?
    //piece types, used to identify board content pieces, 

    //MUST BE powers of 2 so we can mask the bits
    public static int OPEN_SPACE = 0;
    public static int PLAYER = 1;
    public static int BARRIER = 2;
    public static int ZONE_BARRIER = 4;
    public static int PICKUP = 8;
    public static int FOG = 16;

    void FixedUpdate() {
        if (moving) {
            float t = 1.0f - Mathf.Clamp((lerpEndTime - Time.time)/MOVE_DURATION, 0.0f, 1.0f);
            transform.localPosition = Vector2.Lerp(lerpFromTo[0], lerpFromTo[1], t);
            if (t >= 1.0f) {
                moving = false;
            }
        }
    }

    public void SetPosition(int newX, int newY) {
        SetPosition(newX, newY, false);
    }

    public void SetPosition(int newX, int newY, bool lerp)
    {
        //Don't forget to call SetBoard at least once before using this method!
        x = newX;
        y = newY;
        if (lerp) {
            lerpFromTo[0] = transform.localPosition;
            lerpFromTo[1] = board.GetLocalPositionFromCoords(x, y);
            lerpEndTime = Time.time + MOVE_DURATION;
            moving = true;
        } else {
            transform.localPosition = board.GetLocalPositionFromCoords(x, y);
        }

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
    /// Get the pickups under the piece.  This may require searching all board locations so
    /// call isOverPickup() first to determine if there is anything to pickup.
    /// </summary>
    /// <returns>Pieces under current piece location</returns>
    public List<BoardPiece> getPickups() {
        return board.getPickups(x, y);
    }

    /// <summary>
    /// Override to provide the main type of content this piece contains.
    /// Default is none.
    /// </summary>
    /// <returns>Content constant from BoardPiece.cs</returns>
    public virtual int GetContentType() {
        return OPEN_SPACE;
    }

    /// <summary>
    /// Return true if this piece is at the given test location
    /// </summary>
    /// <param name="testX"></param>
    /// <param name="testY"></param>
    /// <returns></returns>
    public bool isAt(int testX, int testY) {
        return testX == x && testY == y;
    }

    public virtual void OnPickup() {

    }

    public virtual void OnRemove() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
