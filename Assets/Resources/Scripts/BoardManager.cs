using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates all boards.
/// </summary>
public class BoardManager : Singleton<BoardManager> {

    // These are manually set so when we make a tutorial we can just toggle them
    public bool relationshipBoardOn = true;
    public bool cashBoardOn = true;
    public bool educationBoardOn = true;
    public bool healthBoardOn = true;
    public int boardSize = 10;

    private Text boardTitleText;

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

    [SerializeField]
    [Tooltip("List of filenames (without path) for each level. Index corresponds to level id")]
    private List<string> levelList = new List<string>();

    //Set current starting level in PermanetStatManager

    public enum BoardType { RELATIONSHIP, CASH, EDUCATION, HEALTH };
    public enum BoardPos { TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT };

    void Start() {
        if (levelLoader == null) {
            Debug.LogError("You didn't add a level loader component to the BoardManager. Hope you enjoy blank levels!");
        }
        if (levelList == null || levelList.Count == 0) {
            Debug.LogError("No levels provided in the levelList");
        }

        tileSize = ResourceLoader.instance.defaultBlockFab.GetComponent<SpriteRenderer>().size.x;
        loadlevel(PermanentStatManager.instance.currentLevel);

        boardTitleText = GameObject.Find("BoardTitle").GetComponent<Text>();

        List<PlayerBoardPiece> playas = new List<PlayerBoardPiece>();
        foreach (Board board in activeBoards) {
            PlayerBoardPiece pbp = board.GetPlayerBoardPiece();
            if (pbp != null) {
                playas.Add(pbp);
            }
            if (board.boardTitle != null && board.boardTitle.Length > 0) {
                boardTitleText.text = board.boardTitle;
            }
        }

        // Need to create board before player.
        PlayerController.instance.Init(playas);
    }

    public List<Board> getAllBoards() {
        return activeBoards;
    }

    private void loadlevel(int levelId) {
        //TODO convert levelId into filenames or something
        Debug.Log("loading level " + levelId);
        if (levelLoader != null) {
            activeBoards.Clear();

            string levelFile = levelList[(levelId % levelList.Count)];
            //data path gives us "Assets"
            ///levels are then at "Resources/Levels/"
            TextAsset levelAsset = ResourceLoader.instance.getLevelTextAsset(levelFile);
            //List<Board> boards =
            //levelLoader.LoadBoardsFromFile(Application.dataPath+"/Resources/Levels/"+levelFile); //FIXME unhardcode
            List<Board> boards =
                levelLoader.LoadBoardsFromBytes(levelAsset.bytes);

            float boardDistance = 2f;
            //position boards correclty
            foreach (Board brd in boards) {
                switch (brd.myBoardType) {
                    case BoardType.RELATIONSHIP:
                        brd.name = "Relationship Board";
                        brd.transform.position = new Vector2(-boardDistance, boardDistance);
                        relationshipBoard = brd;
                        if(relationshipBoardOn)
                            activeBoards.Add(brd);
                        break;
                    case BoardType.CASH:
                        brd.name = "Cash Board";
                        brd.transform.position = new Vector2(-boardDistance, -boardDistance);
                        cashBoard = brd;
                        //NOTE: Attempting to design no randomness for now
                        //brd.gameObject.AddComponent<LootBoardManager>();
                        if (cashBoardOn)
                            activeBoards.Add(brd);
                        break;
                    case BoardType.EDUCATION:
                        brd.name = "Education Board";
                        brd.transform.position = new Vector2(boardDistance, -boardDistance);
                        educationBoard = brd;
                        if(educationBoardOn)
                            activeBoards.Add(brd);
                        break;
                    case BoardType.HEALTH:
                        brd.name = "Health Board";
                        brd.transform.position = new Vector2(boardDistance, boardDistance);
                        healthBoard = brd;
                        if (healthBoardOn)
                            activeBoards.Add(brd);
                        break;

                }
            }
            Debug.Log("loading level " + levelId + " COMPLETE");
        } else {
            Debug.LogError("No level loader");
        }
    }

    /// <summary>
    /// Create a Board
    /// </summary>
    /// <param name="boardSize">Number of tiles (board is square)</param>
    /// <param name="blockPrefab">Prefab for each tile</param>
    /// <param name="bt">Type of board (passed to level loader)</param>
    /// <param name="level">Level Id to load (passed to level loader)</param>
    /// <returns>the board</returns>
    public Board CreateBoard(int boardSize, GameObject blockPrefab, BoardType bt, int level) {
        GameObject newObj = new GameObject();
        Board newBoard = newObj.AddComponent<Board>();
        newBoard.CreateEmptyBoard(boardSize, blockPrefab);
        if (levelLoader != null) {
            levelLoader.LoadBoardContent(newBoard, bt, level); //TODO add in other params for generation as needed
        }
        return newBoard;
    }
}
