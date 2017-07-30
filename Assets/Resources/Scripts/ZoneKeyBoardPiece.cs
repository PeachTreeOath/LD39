using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneKeyBoardPiece : BoardPiece {
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

    public int getKey() {
        return zoneKey;
    }

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }

    public override void OnPickup() {
        foreach (Board bb in BoardManager.instance.getAllBoards()) {
            bb.useKey(this.getKey());
        }
    }
}