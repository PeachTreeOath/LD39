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

        if (btype == BoardManager.BoardType.RELATIONSHIP) {
            for (int y = 0; y < board.GetBoardSize(); y++) {
                GameObject bpOil = Instantiate(ResourceLoader.instance.barrierPieceFab);
                BarrierBoardPiece bp = bpOil.GetComponent<BarrierBoardPiece>();
                bp.SetBoard(board);
                //vertical barrier at x=2
                board.AddPiece(bp, 2, y);
            }
        }
    }
}