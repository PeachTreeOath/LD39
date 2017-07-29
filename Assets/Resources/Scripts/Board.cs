﻿using System.Collections;
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
                block.transform.SetParent(transform);
                block.transform.localPosition = GetLocalPositionFromCoords(j, i);
            }
        }
    }

    public void AddPiece(BoardPiece newPiece)
    {
        boardPieces.Add(newPiece);
    }


    // Return local position given x,y coords. X starts from left, Y starts from bottom. 0-indexed.
    public Vector2 GetLocalPositionFromCoords(int x, int y)
    {
        float currXPos = startingXPos + x * tileSize;
        float currYPos = startingYPos + y * tileSize;
        return new Vector2(currXPos, currYPos);
    }

    // Checks if player can move to this spot. TODO: Modify this to add in obstacle checks.
    public bool IsSquareMovable(int x, int y)
    {
        if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
        {
            return false;
        }
        return true;
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
}
