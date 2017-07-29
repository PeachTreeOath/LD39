using UnityEngine;
using System.Collections;
using System;

public class Potion : TurnBehaviour {

    private int turnsPassed = 8;
    private SpriteRenderer spriteRenderer;

    public int[] turnsPerLevel;
    public int[] healingAtLevel;
    public Sprite[] spriteAtLevel;

    public int level {
        get {
            int turns = turnsPassed;
            for (int i = 0; i < turnsPerLevel.Length; i++) {
                turns -= turnsPerLevel[i];
                if (turns <= 0) return i;
            }

            return turnsPerLevel.Length;
        }
    }

    public int turnsToHeal {
        get {
            int index = Math.Max(level, healingAtLevel.Length - 1);
            return healingAtLevel[index];
        }
    }

    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteAtLevel[level];
    }

    public override void OnAdvanceTurn() {
        turnsPassed++;
        spriteRenderer.sprite = spriteAtLevel[level];
    }
}
