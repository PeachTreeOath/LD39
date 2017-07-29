using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playing board that tracks objects and positions inside it.
/// </summary>
public class Board : MonoBehaviour
{

    [HideInInspector]
    public List<BoardPiece> boardPieces = new List<BoardPiece>();

    private float startingXPos;
    private float startingYPos;
    private float boardSize;
    private float tileSize;

    //array mirrors the board layout and stores a lookup value telling if something
    //(e.g. item, barrier) is at a particular index.
    //X starts from left, Y starts from bottom. 0-indexed.
    //Note index order is [x][y]. Content is BoardPiece.cs constant.
    private int[,] boardContent;

    public void CreateEmptyBoard(int boardSize, GameObject blockPrefab)
    {
        this.boardSize = boardSize;

        tileSize = blockPrefab.GetComponent<SpriteRenderer>().size.x;
        startingXPos = boardSize * -tileSize / 2 + tileSize / 2;
        startingYPos = boardSize * -tileSize / 2 + tileSize / 2;

        boardContent = new int[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject block = Instantiate(blockPrefab);
                BoardPiece bp = block.GetComponent<BoardPiece>();
                if (bp == null) {
                    Debug.LogError("Your prefab or gameobject '" + block.name + "' doesn't have a BoardPiece component - Adding default");
                    bp = block.AddComponent<BoardPiece>();
                }
                if (bp.board == null) {
                    bp.SetBoard(this);
                }
                AddPiece(bp, j, i);
            }
        }
    }

    /// <summary>
    /// Add a piece to this board at the given board location
    /// </summary>
    /// <param name="newPiece">Piece to add. Must be instantiated already.</param>
    /// <param name="boardX">board X coord where the piece will be positioned</param>
    /// <param name="boardY">board Y coord where the piece will be positioned</param>
    public void AddPiece(BoardPiece newPiece, int boardX, int boardY)
    {
        newPiece.gameObject.transform.SetParent(transform);
        newPiece.gameObject.transform.localPosition = GetLocalPositionFromCoords(boardX, boardY);
        boardPieces.Add(newPiece);
        newPiece.SetPosition(boardX, boardY);
    }

    /// <summary>
    /// Remove a piece from this board. Unparents piece but does not destroy it.
    /// </summary>
    /// <param name="piece">Piece to add. Must be instantiated already.</param>
    public void RemovePiece(BoardPiece piece, int curX, int curY)
    {
        piece.gameObject.transform.parent = null;
        UpdatePiece_Remove(piece, curX, curY);
        boardPieces.Remove(piece);
        piece.GetComponent<SpriteRenderer>().enabled = false;
    }

    /// <summary>
    /// Get the pickups, if any, at the given board location.
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    /// <returns>All pickups at location or an empty list if there are none.</returns>
    public List<BoardPiece> getPickups(int xPos, int yPos) {
        List<BoardPiece> pickups = new List<BoardPiece>();

        for (int i = 0; i < boardPieces.Count; i++) {
            int tileContent = boardContent[xPos, yPos];
            if (hasSet(tileContent, BoardPiece.PICKUP)) {
                BoardPiece bp = boardPieces[i];
                if (bp.isAt(xPos, yPos) && bp.GetContentType()==BoardPiece.PICKUP) {
                    pickups.Add(boardPieces[i]);
                    RemovePiece(boardPieces[i], xPos, yPos);
                }
            }
        }

        return pickups;
    }

    public int GetBoardSize() {
        return (int)boardSize;
    }


    // Return local position given x,y coords. X starts from left, Y starts from bottom. 0-indexed.
    public Vector2 GetLocalPositionFromCoords(int x, int y)
    {
        float currXPos = startingXPos + x * tileSize;
        float currYPos = startingYPos + y * tileSize;
        return new Vector2(currXPos, currYPos);
    }

    // Checks if player can move to this spot.
    public bool IsSquareMovable(int x, int y)
    {
        if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
        {
            return false;
        }
        int tileContent = boardContent[x, y];
        if (hasSet(tileContent, BoardPiece.BARRIER)) {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check if the given tile has content (i.e. not empty).
    /// Excludes player and hard barriers in check.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>true if the tile contains something other than the player or a barrier</returns>
    public bool hasTileContent(int x, int y) {
        int contentId = ~(BoardPiece.BARRIER | BoardPiece.PLAYER);
        int tileContent = boardContent[x, y];
        return hasSet(tileContent, contentId);
    }

    /// <summary>
    /// Update the contents of the tiles affected by a piece move.
    /// This version is for moving a piece already on the board.
    /// Does not validate move.
    /// </summary>
    /// <param name="piece">Piece that was moved</param>
    /// <param name="oldX">prev x location</param>
    /// <param name="oldY">prev y location</param>
    /// <param name="newX">new x location</param>
    /// <param name="newY">new y location</param>
    public void UpdatePieceMoved(BoardPiece piece, int oldX, int oldY, int newX, int newY) {
        UpdatePiece_Remove(piece, oldX, oldY);
        UpdatePiece_Place(piece, newX, newY);
    }

    /// <summary>
    /// Update the contents of the tiles affected by a piece move.
    /// This version is for moving a piece off the board.
    /// Does not validate move.
    /// </summary>
    /// <param name="piece">Piece that was moved</param>
    /// <param name="oldX">prev x location</param>
    /// <param name="oldY">prev y location</param>
    public void UpdatePiece_Remove(BoardPiece piece, int oldX, int oldY) {
        int typeId = piece.GetContentType();
        boardContent[oldX, oldY] &= mask_remove(typeId);
    }

    /// <summary>
    /// Update the contents of the tiles affected by a piece move.
    /// This version is for moving a piece NOT already on the board.
    /// Does not validate move.
    /// </summary>
    /// <param name="piece">Piece that was moved</param>
    /// <param name="newX">new x location</param>
    /// <param name="newY">new y location</param>
    public void UpdatePiece_Place(BoardPiece piece, int newX, int newY) {
        int typeId = piece.GetContentType();
        boardContent[newX, newY] |= typeId; 
    }

    //Create a mask that unsets the given value when bitwise-anded
    private int mask_remove(int valToRemove) {
        int mask = ~valToRemove;
        return mask;
    }

    /// <summary>
    /// Test if packedVal contains testVal via bitwise test
    /// </summary>
    /// <param name="packedVal">questionable input</param>
    /// <param name="testVal">testing if in input</param>
    /// <returns>true if packedVal contains testVal</returns>
    private bool hasSet(int packedVal, int testVal) {
        return (packedVal & testVal) > 0;
    }
}
