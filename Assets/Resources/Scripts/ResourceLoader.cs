using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public GameObject defaultBlockFab;
    [HideInInspector]
    public GameObject playerBoardPieceFab;
    [HideInInspector]
    public GameObject dollarBillFab;
    [HideInInspector]
    public GameObject barrierPieceFab;
    [HideInInspector]
    public GameObject potionPieceFab;
    [HideInInspector]
    public GameObject waifuPieceFab;
    [HideInInspector]
    public GameObject eduBookFab;

    [HideInInspector]
    public Sprite portraitKidSprite;
    [HideInInspector]
    public Sprite portraitManSprite;
    [HideInInspector]
    public Sprite portraitOldManSprite;

    protected override void Awake()
    {
        base.Awake();

        LoadResources();
    }

    private void LoadResources()
    {
        defaultBlockFab = Resources.Load<GameObject>("Prefabs/Blocks/DefaultBlock");
        playerBoardPieceFab = Resources.Load<GameObject>("Prefabs/PlayerBoardPiece");
        dollarBillFab = Resources.Load<GameObject>("Prefabs/DollarBill");
        barrierPieceFab = Resources.Load<GameObject>("Prefabs/BarrierBlock");
        potionPieceFab = Resources.Load<GameObject>("Prefabs/Potion");
        waifuPieceFab = Resources.Load<GameObject>("Prefabs/WaifuBlock");
        eduBookFab = Resources.Load<GameObject>("Prefabs/EduBookBlock");

        portraitKidSprite = Resources.Load<Sprite>("Textures/tempFaceKid");
        portraitManSprite = Resources.Load<Sprite>("Textures/tempFaceMan");
        portraitOldManSprite = Resources.Load<Sprite>("Textures/tempFaceOldMan");
    }
}
