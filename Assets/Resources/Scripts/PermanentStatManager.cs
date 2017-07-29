using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentStatManager : Singleton<PermanentStatManager>
{

    public int generation;
    public int wealth;

    protected override void Awake()
    {
        base.Awake();

        SetDontDestroy();
    }

    protected PermanentStatManager() { }


}
