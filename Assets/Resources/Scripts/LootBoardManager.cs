using UnityEngine;
using System.Collections;

public class LootBoardManager : TurnBehaviour {

    const int MAX_TRIES = 100;

    Board lootBoard;

    private int spawnTimer = 0;

    public float[] extraSpawnChances = { 0.5f, 0.25f };
    public float[] extraLevelChances = { 0.5f, 0.5f, 0.5f };
    public int[] levelStackSizes = { 1, 3, 5, 10 };
    public int[] levelValues = { 10, 25, 100, 250 };

    void Start() {
        lootBoard = GetComponent<Board>();
        for(int i = 0; i < 5; i++) {//TODO parameterize
            SpawnLoot();
        }
    }

    public override void OnAdvanceTurn() {
        spawnTimer++;

        int dropRate = Mathf.FloorToInt(LifeStatManager.instance.GetGoldDropRate());
        if (spawnTimer >= dropRate) {
            SpawnLoots();
            spawnTimer = 0;
        } 
    }

    void SpawnLoots() {
        int spawns = 1 + ProgressiveChance(extraSpawnChances);
        for(int i = 0; i < spawns; i++) {
            SpawnLoot();
        }
    }

    void SpawnLoot() {
        int x = 0, y = 0;
        if( FindOpenSpace(ref x, ref y) ) {
            GameObject prefab = Instantiate(ResourceLoader.instance.lootBoardPieceFab);
            LootBoardPiece lootPiece = prefab.GetComponent<LootBoardPiece>();

            int level = ProgressiveChance(extraLevelChances);

            lootPiece.stackSize = levelStackSizes[level];
            lootPiece.value = levelValues[level];
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

    int ProgressiveChance(float[] chances) {
        int count = 0;

        foreach (float chance in chances) {
            if(Random.Range(0f, 1f) > chance) break;
            count++;
        }

        return count;
    }
}
