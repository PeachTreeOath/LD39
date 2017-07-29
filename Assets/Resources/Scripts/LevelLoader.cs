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

            GameObject waifu  = Instantiate(ResourceLoader.instance.waifuPieceFab);
            WaifuBoardPiece wbp = waifu.GetComponent<WaifuBoardPiece>();
            wbp.SetBoard(board);
            //vertical barrier at x=2
            board.AddPiece(wbp, 8, 8);
        }
        if (btype == BoardManager.BoardType.CASH) {
            //dollas at 8,8 - 10,10 
            for (int x = 8; x < 10; x++) {
                for (int y = 8; y < 10; y++) {
                    GameObject tMoney = Instantiate(ResourceLoader.instance.dollarBillFab);
                    LootBoardPiece tm = tMoney.GetComponent<LootBoardPiece>();
                    tm.SetBoard(board);
                    board.AddPiece(tm, x, y);
                }
            }
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
                }
            }
        }
    }
}