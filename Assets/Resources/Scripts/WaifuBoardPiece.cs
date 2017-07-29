using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaifuBoardPiece : BoardPiece {

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }
}