using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBoardPiece : BoardPiece {

    private Potion potion; //TODO this should really all be one script

    public void Start() {
        potion = GetComponent<Potion>();
    }

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }

    public override void OnPickup() {
        LifeStatManager.instance.maxAge += potion.turnsToHeal;
    }
}