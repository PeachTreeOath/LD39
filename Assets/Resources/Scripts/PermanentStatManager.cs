using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentStatManager : Singleton<PermanentStatManager>
{
    public int currentLevel = 0;
    public int generation;
    

    protected override void Awake()
    {
        base.Awake();

        SetDontDestroy();
    }

    protected PermanentStatManager() { }


}
