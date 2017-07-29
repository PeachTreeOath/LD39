using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates all boards.
/// </summary>
public class BoardManager : Singleton<BoardManager>
{

    // These are manually set so when we make a tutorial we can just toggle them
    public bool relationshipBoardOn = true;
    public bool cashBoardOn = true;
    public bool educationBoardOn = true;
    public bool healthBoardOn = true;
    public int boardSize = 10;

    [HideInInspector]
    public Board relationshipBoard;
    [HideInInspector]
    public Board cashBoard;
    [HideInInspector]
    public Board educationBoard;
    [HideInInspector]
    public Board healthBoard;
    [HideInInspector]
    public float tileSize;

    // Need to create board before player.
    void Start()
    {
        tileSize = ResourceLoader.instance.defaultBlockFab.GetComponent<SpriteRenderer>().size.x;

        if (relationshipBoardOn)
        {
            relationshipBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            relationshipBoard.name = "Relationship Board";
            relationshipBoard.transform.position = new Vector2(-2.5f, 2.5f);
        }
        if (cashBoardOn)
        {
            cashBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            cashBoard.name = "Cash Board";
            cashBoard.transform.position = new Vector2(-2.5f, -2.5f);
        }
        if (educationBoardOn)
        {
            educationBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            educationBoard.name = "Education Board";
            educationBoard.transform.position = new Vector2(2.5f, -2.5f);
        }
        if (healthBoardOn)
        {
            healthBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            healthBoard.name = "Health Board";
            healthBoard.transform.position = new Vector2(2.5f, 2.5f);
        }
    }

    public Board CreateBoard(int boardSize, GameObject blockPrefab)
    {
        Board newBoard = gameObject.AddComponent<Board>();
        newBoard.CreateBoard(boardSize, blockPrefab);
        return newBoard;
    }
}
