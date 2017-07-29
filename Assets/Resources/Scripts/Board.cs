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

    public void CreateBoard(int boardSize, GameObject blockPrefab)
    {
        this.boardSize = boardSize;
        tileSize = blockPrefab.GetComponent<SpriteRenderer>().size.x;
        startingXPos = boardSize * -tileSize / 2 + tileSize / 2;
        startingYPos = boardSize * -tileSize / 2 + tileSize / 2;

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
}
