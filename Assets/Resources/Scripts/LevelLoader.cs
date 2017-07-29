using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides level content to the board manager from some TBD source.
/// Level designer will make the levels, this will load 'em into the game.
/// </summary>
public class LevelLoader : MonoBehaviour {


    /// <summary>
    /// Populate a board with content...somehow
    /// </summary>
    /// <param name="board">The board that will have content loaded into it</param>
    /// Params for generation:
    /// <param name="btype">Type of board</param>
    /// <param name="level">Level being loaded</param>
    public void LoadBoardContent(Board board, BoardManager.BoardType btype, int level) {
        if (board == null) {
            Debug.LogError("Invalid board state");
            return;
        }

        //This is test code only
        if (btype == BoardManager.BoardType.RELATIONSHIP) {
            for (int y = 0; y < board.GetBoardSize(); y++) {
                GameObject bpOil = Instantiate(ResourceLoader.instance.barrierPieceFab);
                BarrierBoardPiece bp = bpOil.GetComponent<BarrierBoardPiece>();
                bp.SetBoard(board);
                //vertical barrier at x=2
                board.AddPiece(bp, 2, y);
            }

            GameObject waifu = Instantiate(ResourceLoader.instance.waifuPieceFab);
            WaifuBoardPiece wbp = waifu.GetComponent<WaifuBoardPiece>();
            wbp.SetBoard(board);
            //vertical barrier at x=2
            board.AddPiece(wbp, 8, 8);
        }
        if (btype == BoardManager.BoardType.HEALTH) {
            //health pots wherever
            for (int x = 0; x < 10; x += 3) {
                for (int y = x; y < 8; y += 2) {
                    GameObject horse = Instantiate(ResourceLoader.instance.potionPieceFab);
                    PotionBoardPiece bigH = horse.GetComponent<PotionBoardPiece>();
                    bigH.SetBoard(board);
                    board.AddPiece(bigH, x, y);
                }
            }
        }
        if (btype == BoardManager.BoardType.EDUCATION) {
            for (int x = 6; x <= 8; x++) {
                for (int y = 2; y < board.GetBoardSize() - 2; y++) {
                    GameObject mlady = Instantiate(ResourceLoader.instance.eduBookFab);
                    EduBookBoardPiece ml = mlady.GetComponent<EduBookBoardPiece>();
                    ml.SetBoard(board);
                    //vertical barrier at x=6,7
                    board.AddPiece(ml, x, y);
                    addZoneBarrier(board, 2, 0, 2);
                }
            }
        }
    }

    //Make a square. temp code only
    private void addZoneBarrier(Board board, int zoneKey, int start, int end) {
        List<Vector2> squares = new List<Vector2>();
        //top row
        for (int y = start; y < end; y++) {
            for (int x = start; x < board.GetBoardSize() - start; x++) {
                squares.Add(new Vector2(x, y));
                squares.Add(new Vector2(y, x));
            }
        }
        //bot row
        for (int y = board.GetBoardSize() - end; y < board.GetBoardSize() - start; y++) {
            for (int x = start; x < board.GetBoardSize() - start; x++) {
                squares.Add(new Vector2(x, y));
                squares.Add(new Vector2(y, x));
            }
        }


        foreach (Vector2 v in squares) {
            GameObject vgg = Instantiate(ResourceLoader.instance.zoneBarrierPieceFab);
            ZoneBarrierBoardPiece vvv = vgg.GetComponent<ZoneBarrierBoardPiece>();
            vvv.SetBoard(board);
            vvv.setKey(zoneKey);
            board.AddPiece(vvv, (int)v.x, (int)v.y);
        }
    }


}





