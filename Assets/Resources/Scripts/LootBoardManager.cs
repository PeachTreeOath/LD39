using UnityEngine;
using System.Collections;

public class LootBoardManager : TurnBehaviour {

    const int MAX_TRIES = 100;

    Board lootBoard;

    void Start() {
        lootBoard = GetComponent<Board>();
        for(int i = 0; i < 10; i++) {
            SpawnLoot();
        }
    }

    public override void OnAdvanceTurn() {
    }

    void SpawnLoot() {
        int x = 0, y = 0;
        if( FindOpenSpace(ref x, ref y) ) {
            GameObject prefab = Instantiate(ResourceLoader.instance.dollarBillFab);
            BoardPiece lootPiece = prefab.GetComponent<BoardPiece>();
            lootPiece.SetBoard(lootBoard);
            lootBoard.AddPiece(lootPiece, x, y);
        }
    }

    //TODO generalize this out so it can be reused?
    bool FindOpenSpace(ref int x, ref int y) {
        int size = BoardManager.instance.boardSize;

        bool foundEmpty = false;
        int tries = 0;
        do {
            x = Random.Range(0, size);
            y = Random.Range(0, size);

            foundEmpty = !lootBoard.IsOccupied(x, y);
        } while (!foundEmpty && tries < MAX_TRIES);

        if(!foundEmpty) {
            x = y = -1;
        }

        return foundEmpty;
    }
}
