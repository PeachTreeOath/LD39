using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    private float tileSize;

    public void CreateBoard(int boardSize, GameObject blockPrefab)
    {
        GameObject board = new GameObject();

        float startingXPos = boardSize * -tileSize / 2;
        float startingYPos = boardSize * -tileSize / 2;

        tileSize = blockPrefab.GetComponent<SpriteRenderer>().size.x;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject block = Instantiate(blockPrefab);
                block.transform.SetParent(board.transform);
                //block.transform.lco
            }
        }
    }

}
