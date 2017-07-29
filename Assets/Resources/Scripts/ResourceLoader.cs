using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public GameObject defaultBlockFab;

    [HideInInspector]
    public GameObject dollarBillFab;

    protected override void Awake()
    {
        base.Awake();

        LoadResources();
    }

    private void LoadResources()
    {
        defaultBlockFab = Resources.Load<GameObject>("Prefabs/Blocks/DefaultBlock");
        dollarBillFab = Resources.Load<GameObject>("Prefabs/DollarBill");
    }
}
