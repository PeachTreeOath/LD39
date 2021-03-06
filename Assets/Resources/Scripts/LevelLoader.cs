﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides level content to the board manager from some TBD source.
/// Level designer will make the levels, this will load 'em into the game.
/// Generally this will be assigned on the BoardManager via inspector.
/// </summary>
[RequireComponent(typeof(LevelFileIO))]
public class LevelLoader : MonoBehaviour {

    /// <summary>
    /// Load level from predefined file
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public List<Board> LoadBoardsFromFile(string filename) {
        LevelFileIO lfio = gameObject.GetComponent<LevelFileIO>();
        if (lfio == null) {
            Debug.LogError("You will need a fileIO to load a file");
            return null;
        }
        return lfio.loadLevel(filename);
    }

    /// <summary>
    /// Load level from bytes (TextAsset bytes)
    /// </summary>
    public List<Board> LoadBoardsFromBytes(byte[] filebytes) {
        LevelFileIO lfio = gameObject.GetComponent<LevelFileIO>();
        if (lfio == null) {
            Debug.LogError("You will need a fileIO to load a file");
            return null;
        }
        return lfio.loadLevel(filebytes);
    }

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

            //addKey(board, 0, 3, 3);
            //addKey(board, 1, 4, 5);
            //addKey(board, 2, 5, 8);
        }
        if (btype == BoardManager.BoardType.HEALTH) {
            //health pots wherever
            for (int x = 0; x < 10; x += 3) {
                for (int y = x; y < 8; y += 2) {
                    GameObject horse = Instantiate(ResourceLoader.instance.potionPieceFab);
                    PotionBoardPiece bigH = horse.GetComponent<PotionBoardPiece>();
                    bigH.SetBoard(board);
                    board.AddPiece(bigH, x, y);
                    addZoneBarrier(board, 0, 0, 1);
                    addZoneBarrier(board, 1, 1, 2);
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
                Vector2 va = new Vector2(x, y);
                if (!squares.Contains(va)) {
                    squares.Add(va);
                }
                Vector2 vb = new Vector2(y, x);
                if (!squares.Contains(vb)) {
                    squares.Add(vb);
                }
            }
        }
        //bot row
        for (int y = board.GetBoardSize() - end; y < board.GetBoardSize() - start; y++) {
            for (int x = start; x < board.GetBoardSize() - start; x++) {
                Vector2 va = new Vector2(x, y);
                if (!squares.Contains(va)) {
                    squares.Add(va);
                }
                Vector2 vb = new Vector2(y, x);
                if (!squares.Contains(vb)) {
                    squares.Add(vb);
                }
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

    private void addKey(Board board, int key, int x, int y) {
        GameObject keyObj = Instantiate(ResourceLoader.instance.zoneKeyPieceFab);
        ZoneKeyBoardPiece zk = keyObj.GetComponent<ZoneKeyBoardPiece>();
        zk.SetBoard(board);
        zk.setKey(key);
        board.AddPiece(zk, x, y);
    }


}





