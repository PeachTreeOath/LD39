using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

/// <summary>
/// Read/write level files
/// </summary>
public class LevelFileIO : MonoBehaviour {

    private static int boardSize = 10;

    private static string BOARD_TYPE = "boardType:\t";
    private static string FOG_SIZE = "fogSize:\t";
    private static string BOARD_POS = "boardPos:\t";
    private static string START_BOARD = "startdata:\t";
    private static string END_BOARD = "enddata:\t";
    private static string COMMENT = "#";

    private static BoardManager.BoardType[] allTypes = new BoardManager.BoardType[]{
            BoardManager.BoardType.RELATIONSHIP,
            BoardManager.BoardType.HEALTH,
            BoardManager.BoardType.CASH,
            BoardManager.BoardType.EDUCATION
            };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public BoardManager.BoardPos getDefaultBoardPos(BoardManager.BoardType bType) {
        switch (bType) {
            case BoardManager.BoardType.RELATIONSHIP:
                return BoardManager.BoardPos.TOP_LEFT;
            case BoardManager.BoardType.HEALTH:
                return BoardManager.BoardPos.TOP_RIGHT;
            case BoardManager.BoardType.CASH:
                return BoardManager.BoardPos.BOTTOM_LEFT;
            case BoardManager.BoardType.EDUCATION:
                return BoardManager.BoardPos.BOTTOM_RIGHT;
        }
        Debug.LogError("WTF are you doing");
        return BoardManager.BoardPos.TOP_LEFT; //cant return null
    }

    /// <summary>
    /// Output a blank level template that can be filled in by hand
    /// </summary>
    /// <param name="filename"></param>
    public void createNewTemplate(string filename) {
        Debug.Log("Writing new template level to " + filename);
        StreamWriter sw = new StreamWriter(filename);
        using (sw) {
            foreach (BoardManager.BoardType boardType in allTypes) {
                writeHeader(sw, boardType, 0, getDefaultBoardPos(boardType));
                writeTemplateBoard(sw, boardSize);
            }
            sw.Close();

        }
    }

    private void writeHeader(StreamWriter sw,
        BoardManager.BoardType boardType, int fogSize, BoardManager.BoardPos boardPos) {
        sw.WriteLine(BOARD_TYPE + boardType.ToString());
        sw.WriteLine(FOG_SIZE + fogSize.ToString());
        sw.WriteLine(BOARD_POS + boardPos.ToString());
    }

    private void writeTemplateBoard(StreamWriter sw,
        int boardSize) {
        sw.WriteLine(START_BOARD);
        sw.Write(COMMENT);
        for (int i = 0; i < boardSize; i++) {
            sw.Write("" + i + ",\t ");
        }
        sw.WriteLine();
        for (int y = 0; y < boardSize; y++) {
            for (int x = 0; x < boardSize; x++) {
                sw.Write(" _,\t");
            }
            sw.WriteLine(" " + COMMENT + y);
        }
        sw.WriteLine(END_BOARD);
        sw.WriteLine();
    }
}
