using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPiece : BoardPiece {

    public override int GetContentType() {
        return base.BARRIER;
    }
}