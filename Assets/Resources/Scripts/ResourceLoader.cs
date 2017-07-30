using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public GameObject defaultBlockFab;
    [HideInInspector]
    public GameObject fogBlockFab;
    [HideInInspector]
    public GameObject playerBoardPieceFab;
    [HideInInspector]
    public GameObject lootBoardPieceFab;
    [HideInInspector]
    public GameObject dollarBillFab;
    [HideInInspector]
    public GameObject priceTagFab;
    [HideInInspector]
    public GameObject barrierPieceFab;
    [HideInInspector]
    public GameObject zoneBarrierPieceFab;
    [HideInInspector]
    public GameObject zoneKeyPieceFab;
    [HideInInspector]
    public GameObject potionPieceFab;
    [HideInInspector]
    public GameObject waifuPieceFab;
    [HideInInspector]
    public GameObject eduBookFab;
    [HideInInspector]
    public GameObject scrollBGFab;

    [HideInInspector]
    public Sprite portraitKidSprite;
    [HideInInspector]
    public Sprite portraitManSprite;
    [HideInInspector]
    public Sprite portraitOldManSprite;
    [HideInInspector]
    public Sprite portraitFemaleASprite;

    [HideInInspector]
    public Sprite girlBodySprite;
    [HideInInspector]
    public Sprite[] girlDressSprites;
    [HideInInspector]
    public Sprite[] girlHairSprites;
    [HideInInspector]
    public Sprite girlEyesSprite;
    [HideInInspector]
    public Sprite girlShadowSprite;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {
        girlBodySprite = Resources.Load<Sprite>("Textures/lady-bits/girl-front-body 1");
        girlShadowSprite = Resources.Load<Sprite>("Textures/lady-bits/girl-front-shadow");
        girlEyesSprite = Resources.Load<Sprite>("Textures/lady-bits/girl-front-eyes");

        girlDressSprites = new Sprite[5];
        for (int i = 0; i < girlDressSprites.Length; i++) {
            string path = string.Format("Textures/lady-bits/girl-front-dress-{0}", i + 1);
            girlDressSprites[i] = Resources.Load<Sprite>(path); 
        }

        girlHairSprites = new Sprite[6];
        for (int i = 0; i < girlHairSprites.Length; i++) {
            string path = string.Format("Textures/lady-bits/girl-front-hair-{0}", i + 1);
            girlHairSprites[i] = Resources.Load<Sprite>(path);
        }

        defaultBlockFab = Resources.Load<GameObject>("Prefabs/Blocks/DefaultBlock");
        fogBlockFab = Resources.Load<GameObject>("Prefabs/Blocks/FogBlock");
        playerBoardPieceFab = Resources.Load<GameObject>("Prefabs/PlayerBoardPiece");
        lootBoardPieceFab = Resources.Load<GameObject>("Prefabs/LootBoardPiece");
        dollarBillFab = Resources.Load<GameObject>("Prefabs/DollarBill");
        priceTagFab = Resources.Load<GameObject>("Prefabs/PriceTag");
        barrierPieceFab = Resources.Load<GameObject>("Prefabs/BarrierBlock");
        zoneBarrierPieceFab = Resources.Load<GameObject>("Prefabs/ZoneBarrierBlock");
        zoneKeyPieceFab = Resources.Load<GameObject>("Prefabs/ZoneKeyBlock");
        potionPieceFab = Resources.Load<GameObject>("Prefabs/Potion");
        waifuPieceFab = Resources.Load<GameObject>("Prefabs/WaifuBlock");
        eduBookFab = Resources.Load<GameObject>("Prefabs/EduBookBlock");
        scrollBGFab = Resources.Load<GameObject>("Prefabs/ScrollingBG");

        portraitKidSprite = Resources.Load<Sprite>("Textures/tempFaceKid");
        portraitManSprite = Resources.Load<Sprite>("Textures/tempFaceMan");
        portraitOldManSprite = Resources.Load<Sprite>("Textures/tempFaceOldMan");
        portraitFemaleASprite = Resources.Load<Sprite>("Textures/tempGrill");


    }
}
