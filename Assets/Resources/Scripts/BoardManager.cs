using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public bool relationshipBoardOn = true;
    public bool cashBoardOn = true;
    public bool educationBoardOn = true;
    public bool healthBoardOn = true;
    public int boardSize = 10;

    private float tileSize;

    // Use this for initialization
    void Start()
    {
        if (relationshipBoardOn)
        {
            GameObject relationshipBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            relationshipBoard.name = "Relationship Board";
            relationshipBoard.transform.position = new Vector2(-2.5f, 2.5f);
        }
        if (cashBoardOn)
        {
            GameObject cashBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            cashBoard.name = "Cash Board";
            cashBoard.transform.position = new Vector2(-2.5f, -2.5f);
        }
        if (educationBoardOn)
        {
            GameObject educationBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            educationBoard.name = "Education Board";
            educationBoard.transform.position = new Vector2(2.5f, -2.5f);
        }
        if (healthBoardOn)
        {
            GameObject healthBoard = CreateBoard(boardSize, ResourceLoader.instance.defaultBlockFab);
            healthBoard.name = "Health Board";
            healthBoard.transform.position = new Vector2(2.5f, 2.5f);
        }
    }

    public GameObject CreateBoard(int boardSize, GameObject blockPrefab)
    {
        GameObject board = new GameObject();

        tileSize = blockPrefab.GetComponent<SpriteRenderer>().size.x;
        float startingXPos = boardSize * -tileSize / 2 + tileSize / 2;
        float startingYPos = boardSize * -tileSize / 2 + tileSize / 2;

        for (int i = 0; i < boardSize; i++)
        {
            float currYPos = startingYPos + i * tileSize;
            for (int j = 0; j < boardSize; j++)
            {
                float currXPos = startingXPos + j * tileSize;

                GameObject block = Instantiate(blockPrefab);
                block.transform.SetParent(board.transform);
                block.transform.localPosition = new Vector2(currXPos, currYPos);
            }
        }

        return board;
    }
}
