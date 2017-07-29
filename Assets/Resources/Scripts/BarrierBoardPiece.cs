using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBoardPiece : BoardPiece {

    public override int GetContentType() {
        return BoardPiece.BARRIER;
    }
}