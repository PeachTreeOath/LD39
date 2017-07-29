﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoardPiece : BoardPiece {

    [SerializeField]
    private int size = 0;

    public int value = 10;
    public float dollarSpacing = 0.03f;

    public int stackSize {
        set {
            if(size != value) {
                size = value;
                CreateStack(size);
            }
        }

        get { return size; }
    }

    void Start() {
        CreateStack(size);
    }

    public void CreateStack(int size) {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        float y = 0;
        for(int i = 0; i < size; i++) {
            GameObject dollar = Instantiate(ResourceLoader.instance.dollarBillFab);
            dollar.transform.SetParent(transform);
            dollar.transform.localPosition = new Vector2(0, y);

            y += dollarSpacing;
        }
        
    }

    public override int GetContentType() {
        return BoardPiece.PICKUP;
    }

    public override void OnPickup() {
        PermanentStatManager.instance.wealth += value;
    }
}
