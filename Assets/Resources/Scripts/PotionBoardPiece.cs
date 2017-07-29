using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBoardPiece : BoardPiece {

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }
}