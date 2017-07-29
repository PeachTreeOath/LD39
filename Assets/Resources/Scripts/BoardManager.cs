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

    private List<Board> activeBoards = new List<Board>();

    [SerializeField]
    private LevelLoader levelLoader;

    public enum BoardType { RELATIONSHIP, CASH, EDUCATION, HEALTH };

    void Start()
    {
        if (levelLoader == null) {
            Debug.LogError("You didn't add a level loader component to the BoardManager. Hope you enjoy blank levels!");
        }

        tileSize = ResourceLoader.instance.defaultBlockFab.GetComponent<SpriteRenderer>().size.x;

        if (relationshipBoardOn)
        {
            relationshipBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab, BoardType.RELATIONSHIP, 1);
            relationshipBoard.name = "Relationship Board";
            relationshipBoard.transform.position = new Vector2(-2.5f, 2.5f);
            activeBoards.Add(relationshipBoard);
        }
        if (cashBoardOn)
        {
            cashBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab, BoardType.CASH, 1);
            cashBoard.name = "Cash Board";
            cashBoard.transform.position = new Vector2(-2.5f, -2.5f);
            cashBoard.gameObject.AddComponent<LootBoardManager>();
            activeBoards.Add(cashBoard);
        }
        if (educationBoardOn)
        {
            educationBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab, BoardType.EDUCATION, 1);
            educationBoard.name = "Education Board";
            educationBoard.transform.position = new Vector2(2.5f, -2.5f);
            activeBoards.Add(educationBoard);
        }
        if (healthBoardOn)
        {
            healthBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab, BoardType.HEALTH, 1);
            healthBoard.name = "Health Board";
            healthBoard.transform.position = new Vector2(2.5f, 2.5f);
            activeBoards.Add(healthBoard);
        }

        // Need to create board before player.
        PlayerController.instance.Init();
    }

    public List<Board> getAllBoards() {
        return activeBoards;
    }

    /// <summary>
    /// Create a Board
    /// </summary>
    /// <param name="boardSize">Number of tiles (board is square)</param>
    /// <param name="blockPrefab">Prefab for each tile</param>
    /// <param name="bt">Type of board (passed to level loader)</param>
    /// <param name="level">Level Id to load (passed to level loader)</param>
    /// <returns>the board</returns>
    public Board CreateBoard(int boardSize, GameObject blockPrefab, BoardType bt, int level)
    {
        GameObject newObj = new GameObject();
        Board newBoard = newObj.AddComponent<Board>();
        newBoard.CreateEmptyBoard(boardSize, blockPrefab);
        if (levelLoader != null) {
            levelLoader.LoadBoardContent(newBoard, bt, level); //TODO add in other params for generation as needed
        }
        return newBoard;
    }
}
