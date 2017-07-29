using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoardPiece : BoardPiece {

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }
}