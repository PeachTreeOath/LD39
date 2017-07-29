using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EduBookBoardPiece : BoardPiece {

    public int bookValue = 1;

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }
}