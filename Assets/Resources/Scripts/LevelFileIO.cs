using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
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

    //letter denotes block contents
    //parameters can be provided in square brackets if needed
    // e.g.: M[4]Z 
    // e.g.2: H[4,5]CX

    //private static string KEY_ORANGE = ; //prob wont need
    private const char BARRIER_ORANGE = 'Z';
    private const char BARRIER_PURPLE = 'X';
    private const char BARRIER_GREEN = 'C';

    private const char STATIC_BARRIER = 'Q';

    private const char PLAYER = 'P';
    private const char BOOK = 'B';
    private const char MONEY = 'M';
    private const char WAIFU = 'W';
    private const char HEALTH = 'H';

    private const char NOOP = '_';
    private const char NOOP1 = ' ';
    private const char NOOP2 = '\t';
    private const char PARAM_START = '[';
    private const char PARAM_END = ']';
    private const char FS_END = ',';

    private static BoardManager.BoardType[] allTypes = new BoardManager.BoardType[]{
            BoardManager.BoardType.RELATIONSHIP,
            BoardManager.BoardType.HEALTH,
            BoardManager.BoardType.CASH,
            BoardManager.BoardType.EDUCATION
            };

    private Board curBoard; //board being built

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
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
            writeHeaderHelp(sw);
            foreach (BoardManager.BoardType boardType in allTypes) {
                writeHeader(sw, boardType, 0, getDefaultBoardPos(boardType));
                writeTemplateBoard(sw, boardSize);
            }
            sw.Close();

        }
    }

    private void writeHeaderHelp(StreamWriter sw) {
        sw.WriteLine("#Help tips: Replace board underscores with 1 or more letters below");
        sw.WriteLine("#Note, some combinations cannot be stacked on the same tile");
        sw.WriteLine("#BARRIER_ORANGE = 'Z'");
        sw.WriteLine("#BARRIER_PURPLE = 'X'");
        sw.WriteLine("#BARRIER_GREEN = 'C'");
        sw.WriteLine("#STATIC_BARRIER = 'Q'");
        sw.WriteLine("#PLAYER = 'P'");
        sw.WriteLine("#BOOK = 'B'");
        sw.WriteLine("#MONEY = 'M'  (include one param [startingValue])");
        sw.WriteLine("#WAIFU = 'W'");
        sw.WriteLine("#HEALTH = 'H'");
        sw.WriteLine("#NOOP (ignored) = '_'");
        sw.WriteLine("#NOOP1  (ignored) = ' '");
        sw.WriteLine("#NOOP2  (tab ignored) = '\t'");
        sw.WriteLine("#Parameters are applied in a bracket after certain letters: e.g. H[4] or W[1,2]");
        sw.WriteLine("#PARAM_START = '['");
        sw.WriteLine("#PARAM_END = ']'");
        sw.WriteLine("#FS_END (column separator) = ','");
        sw.WriteLine("#end of help");
        sw.WriteLine();
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
            sw.Write("   " + i + ",\t  ");
        }
        sw.WriteLine();
        for (int y = 0; y < boardSize; y++) {
            for (int x = 0; x < boardSize; x++) {
                sw.Write("   __,\t");
            }
            sw.WriteLine(" " + COMMENT + y);
        }
        sw.WriteLine(END_BOARD);
        sw.WriteLine();
    }

    /// <summary>
    /// Load a set of boards that make up a level from the given file.  Returns 
    /// the loaded boards.  Boards need to be positioned after return.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public List<Board> loadLevel(string filename) {
        Debug.Log("LoadLevel " + filename);
            StreamReader sr = new StreamReader(filename, Encoding.Default);
        return readLevelFile(sr);
    }

    /// <summary>
    /// Load a set of boards that make up a level from the given file.  Returns 
    /// the loaded boards.  Boards need to be positioned after return.
    /// </summary>
    /// <param name="fileBytes">File text in byte array</param>
    /// <returns></returns>
    public List<Board> loadLevel(byte[] fileBytes) {
        Debug.Log("LoadLevel from bytes");
        MemoryStream ms = new MemoryStream(fileBytes);
        StreamReader sr = new StreamReader(ms);
        return readLevelFile(sr);
    }

    //read a bunch of boards 
    private List<Board> readLevelFile(StreamReader sr) {
        List<Board> levelBoards = new List<Board>();
        try {
            //StreamReader sr = new StreamReader(filename, Encoding.Default);
            using (sr) {
                bool boardReadComplete = false;
                string boardTypeStr = "";
                string fogSizeStr = "";
                string boardPosStr = "";

                string line = sr.ReadLine();
                while (line != null) {
                    line = line.Trim();
                    if (line.StartsWith(COMMENT) || line.Length == 0) {
                        line = sr.ReadLine();
                        continue;
                    } else if (line.StartsWith(BOARD_TYPE)) {
                        boardTypeStr = line.Replace(BOARD_TYPE, "").Trim().ToUpper();
                    } else if (line.StartsWith(FOG_SIZE)) {
                        fogSizeStr = line.Replace(FOG_SIZE, "").Trim();
                    } else if (line.StartsWith(BOARD_POS)) {
                        boardPosStr = line.Replace(BOARD_POS, "").Trim().ToUpper();
                    } else if (line.StartsWith(START_BOARD.Trim())) {
                        //Debug.Log("start board");
                        GameObject newBoardObj = new GameObject();
                        curBoard = newBoardObj.AddComponent<Board>();
                        float tileSize = ResourceLoader.instance.defaultBlockFab.GetComponent<SpriteRenderer>().size.x;
                        curBoard.CreateEmptyBoardWithoutTiles(boardSize, tileSize);
                        readBoard(sr);
                        boardReadComplete = true;
                    } else if (line.StartsWith(END_BOARD.Trim())) {
                        //normally this is read in during the readBoard
                        boardReadComplete = true;
                    }

                    if (boardReadComplete) {
                        BoardManager.BoardType mbt = (BoardManager.BoardType)Enum.Parse(typeof(BoardManager.BoardType), boardTypeStr);
                        curBoard.myBoardType = mbt;
                        levelBoards.Add(curBoard);

                        curBoard = null;
                        boardReadComplete = false;
                        boardTypeStr = "";
                        fogSizeStr = "";
                        boardPosStr = "";
                    }
                    line = sr.ReadLine();
                }

                sr.Close();
            }//using
        } catch (Exception ex) {
            Debug.LogError("Error during level load of board " + levelBoards.Count);
            throw ex;
        }

        Debug.Log("Loaded " + levelBoards.Count + " boards");
        return levelBoards;
    }

    //read a full set of board data. These pieces are stored in the global curBoard
    private void readBoard(StreamReader sr) {
        string line = sr.ReadLine();
        //each line in the board is ROW with all COLs for that row on the line
        List<BoardPiece> pcs = new List<BoardPiece>();
        int rowCount = 0;
        while (line != null) {
            line = line.Trim();
            if (line.StartsWith(COMMENT) || line.Length == 0) {
                line = sr.ReadLine();
                continue;
            } else if (line.StartsWith(END_BOARD.Trim())) {
                //end of this board
                //Debug.Log("Read " + rowCount + " board lines");
                break;
            } else {
                string[] parts = line.Trim().Split(',');
                //note that rows are actually numbered bottom to top
                pcs.AddRange(readPieces(parts, (boardSize - rowCount - 1)));
                //Debug.Log("Row done");
                rowCount++;
            }

            line = sr.ReadLine();

        }
        Debug.Log("Board finished loading with " + pcs.Count + " pieces");
    }

    //read each column of a board row. pieces are stored in global curBoard.
    private List<BoardPiece> readPieces(string[] cells, int rowNum) {
        List<BoardPiece> pcs = new List<BoardPiece>();
        //each cell should represent a column
        //with the exception of comments
        int colCount = 0;
        foreach (string cell in cells) {
            //Debug.Log("cellText: " + cell);
            try {
                string c = cell.Trim().ToUpper();
                if (c.StartsWith("#") || c.Length == 0) {
                    continue;
                }
                //there is always a blank cell under everything
                pcs.Add(createBlank(rowNum, colCount, null));
                BoardPiece newPc = null;
                for (int i = 0; i < c.Length; i++) {
                    //Debug.Log("nextChar: " + c[i]);
                    newPc = null;
                    switch (c[i]) {
                        case NOOP:
                        case NOOP1:
                        case NOOP2:
                            break;

                        case BARRIER_ORANGE:
                            newPc = createOrangeBarrier(rowNum, colCount, null);
                            break;
                        case BARRIER_PURPLE:
                            newPc = createPurpleBarrier(rowNum, colCount, null);
                            break;
                        case BARRIER_GREEN:
                            newPc = createGreenBarrier(rowNum, colCount, null);
                            break;

                        case STATIC_BARRIER:
                            newPc = createStaticBarrier(rowNum, colCount, null);
                            break;

                        case PLAYER:
                            if (peekParam(c, i)) {
                                List<int> prams = readTileParams(c, i);
                                i = prams[0];
                                prams.RemoveAt(0);
                                newPc = createPlayer(rowNum, colCount, prams);
                            } else {
                                newPc = createPlayer(rowNum, colCount, null);
                            }
                            break;

                        case BOOK:
                            if (peekParam(c, i)) {
                                List<int> prams = readTileParams(c, i);
                                i = prams[0];
                                prams.RemoveAt(0);
                                newPc = createBook(rowNum, colCount, prams);
                            } else {
                                newPc = createBook(rowNum, colCount, null);
                            }
                            break;

                        case MONEY:
                            if (peekParam(c, i)) {
                                List<int> prams = readTileParams(c, i);
                                i = prams[0];
                                prams.RemoveAt(0);
                                newPc = createMoney(rowNum, colCount, prams);
                            } else {
                                newPc = createMoney(rowNum, colCount, null);
                            }
                            break;

                        case WAIFU:
                            if (peekParam(c, i)) {
                                List<int> prams = readTileParams(c, i);
                                i = prams[0];
                                prams.RemoveAt(0);
                                newPc = createWaifu(rowNum, colCount, prams);
                            } else {
                                newPc = createWaifu(rowNum, colCount, null);
                            }
                            break;

                        case HEALTH:
                            if (peekParam(c, i)) {
                                List<int> prams = readTileParams(c, i);
                                i = prams[0];
                                prams.RemoveAt(0);
                                newPc = createHealth(rowNum, colCount, prams);
                            } else {
                                newPc = createHealth(rowNum, colCount, null);
                            }
                            break;
                    }//switch
                    if (newPc != null) {
                        pcs.Add(newPc);
                    }
                }
                colCount++;

            } catch (Exception ex) {
                Debug.LogError("Error processing board row,col = " + rowNum + "," + colCount);
                throw ex;
            }
        }//foreach

        return pcs;
    }

    //check if idx of str is the start of a parameter
    //Note, str is typically a char followed by a param so we 
    //check idx+1 for the start of the parameter
    private bool peekParam(string str, int idx) {
        if (idx + 1< str.Length) {
            return str[idx+1] == PARAM_START;
        }
        return false;
    }

    //Read tile params starting at the given index. 
    //Start index must be param_start.  This expects a list of comma separated
    //numbers between the param_start and param_end.
    //Returns a list of integers where
    //THE FIRST NUM is the str idx at the end of the params (e.g. right before where next read begins)
    private List<int> readTileParams(string str, int idx) {
        idx = idx + 1;  //hack cuz the count was starting before every param
        List<int> results = new List<int>();
        try {
            int endidx = idx;
            if (idx >= str.Length) {
                results.Add(endidx);
                return results;
            }

            //find next end of param
            for (int i = idx; i < str.Length; i++) {
                if (str[i] == PARAM_END) {
                    endidx = i;
                    break;
                }
            }
            results.Add(endidx);

            string[] numsStr = str.Substring(idx + 1, endidx - idx - 1).Split(',');
            foreach (string num in numsStr) {
                results.Add(int.Parse(num.Trim()));
            }
        } catch (Exception ex) {
            Debug.Log("File read error for param string " + str);
            throw ex;
        }

        return results;
    }

    private BoardPiece createBlank(int rowNum, int colCount, List<int> prams) {
        GameObject bGo = Instantiate(ResourceLoader.instance.defaultBlockFab);
        BoardPiece pc = bGo.GetComponent<BoardPiece>();
        pc.SetBoard(curBoard); //maybe this could go into AddPiece ?!?!?!
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }

    private BoardPiece createZoneBarrier(int rowNum, int colCount, int colorType) {
        GameObject bGo = Instantiate(ResourceLoader.instance.zoneBarrierPieceFab);
        ZoneBarrierBoardPiece pc = bGo.GetComponent<ZoneBarrierBoardPiece>();
        pc.SetBoard(curBoard);
        pc.setKey(colorType);
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }

    private BoardPiece createOrangeBarrier(int rowNum, int colCount, List<int> prams) {
        return createZoneBarrier(rowNum, colCount, 1);
    }

    private BoardPiece createPurpleBarrier(int rowNum, int colCount, List<int> prams) {
        return createZoneBarrier(rowNum, colCount, 2);
    }

    private BoardPiece createGreenBarrier(int rowNum, int colCount, List<int> prams) {
        return createZoneBarrier(rowNum, colCount, 0);
    }

    private BoardPiece createStaticBarrier(int rowNum, int colCount, List<int> prams) {
        GameObject bGo = Instantiate(ResourceLoader.instance.barrierPieceFab);
        BarrierBoardPiece pc = bGo.GetComponent<BarrierBoardPiece>();
        pc.SetBoard(curBoard);
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }

    private BoardPiece createFog(int rowNum, int colCount, List<int> prams) {
        //TODO
        return null;
    }

    private BoardPiece createPlayer(int rowNum, int colCount, List<int> prams) {
        //TOD PARAMs
        PlayerBoardPiece newPlayer = Instantiate(ResourceLoader.instance.playerBoardPieceFab)
            .GetComponent<PlayerBoardPiece>();
        newPlayer.SetBoard(curBoard);
        curBoard.AddPiece(newPlayer, colCount, rowNum);
        return newPlayer;
    }
    private BoardPiece createBook(int rowNum, int colCount, List<int> prams) {
        //TODO params
        GameObject bGo = Instantiate(ResourceLoader.instance.eduBookFab);
        EduBookBoardPiece pc = bGo.GetComponent<EduBookBoardPiece>();
        pc.SetBoard(curBoard);
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }
    private BoardPiece createMoney(int rowNum, int colCount, List<int> prams) {
        //TODO params
        GameObject bGo = Instantiate(ResourceLoader.instance.lootBoardPieceFab);
        LootBoardPiece pc = bGo.GetComponent<LootBoardPiece>();
        pc.SetBoard(curBoard);
        if (prams != null)
        {
            //expecting 1 num = value of drop
            if (prams.Count >= 1)
            {
                int dropVal = prams[0];
                Debug.Log("money value is " + dropVal);
                pc.value = dropVal;
            }
        }
        else
            pc.value = 25;
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }
    private BoardPiece createWaifu(int rowNum, int colCount, List<int> prams) {
        //TODO params
        GameObject bGo = Instantiate(ResourceLoader.instance.waifuPieceFab);
        WaifuBoardPiece pc = bGo.GetComponent<WaifuBoardPiece>();
        pc.SetBoard(curBoard);
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }
    private BoardPiece createHealth(int rowNum, int colCount, List<int> prams) {
        //TODO params
        GameObject bGo = Instantiate(ResourceLoader.instance.potionPieceFab);
        PotionBoardPiece pc = bGo.GetComponent<PotionBoardPiece>();
        pc.SetBoard(curBoard);
        curBoard.AddPiece(pc, colCount, rowNum);
        return pc;
    }
}

