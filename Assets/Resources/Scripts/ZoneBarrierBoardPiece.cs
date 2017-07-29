using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBarrierBoardPiece : BoardPiece {
    //A key with the same id will unlock the matching zone
    public int zoneKey = 0;

    public Sprite[] spriteForKey;

    void Start() {
        setKey(zoneKey);
    }

    public void setKey(int newKey) {
        if(newKey < 0 || newKey >= spriteForKey.Length) {
            Debug.LogError("Invalid zone key - no sprite for key " + newKey);
            return;
        }
        zoneKey = newKey;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteForKey[newKey];
    }

    public void unlock(int key) {
        if (key == zoneKey) {
            board.RemovePiece(this, x, y);
        }
    }

    public override int GetContentType() {
        return BoardPiece.ZONE_BARRIER;
    }
}