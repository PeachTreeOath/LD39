using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentStatManager : Singleton<PermanentStatManager>
{
    public int currentLevel = 0;
    public int startingWealth;

    private int _wealth = 0; // :-(

    public int generation;
    public int wealth {
        get { return _wealth; }
        set {
            if(_wealth != value) {
                _wealth = value;

                BuyablePiece[] buyables = Object.FindObjectsOfType<BuyablePiece>();
                foreach(BuyablePiece buyable in buyables) {
                    buyable.OnWealthChange();
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _wealth = startingWealth;
        SetDontDestroy();
    }

    protected PermanentStatManager() { }


}
