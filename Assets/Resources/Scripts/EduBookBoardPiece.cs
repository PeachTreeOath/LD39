using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EduBookBoardPiece : BoardPiece {

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }
}