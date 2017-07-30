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
    public Sprite[] girlDressSprites;
    [HideInInspector]
    public Sprite[] girlHairSprites;
    [HideInInspector]
    public Sprite[] portraitGirlDressSprites;
    [HideInInspector]
    public Sprite[] portraitGirlHairSprites;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {
        girlDressSprites = LoadSpriteArray("Textures/lady-bits/girl-front-dress-{0}", 5);
        girlHairSprites = LoadSpriteArray("Textures/lady-bits/girl-front-hair-{0}", 6);
        portraitGirlHairSprites = LoadSpriteArray("Textures/big-lady-bits/girl-front-dress-{0}", 5);
        portraitGirlDressSprites = LoadSpriteArray("Textures/big-lady-bits/girl-front-hair-{0}", 6);

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

    private Sprite[] LoadSpriteArray(string format, int count) {
        Sprite[] sprites = new Sprite[count];
        for (int i = 0; i < count;  i++) {
            string path = string.Format(format, i + 1);
            sprites[i] = Resources.Load<Sprite>(path);
        }

        return sprites;
    }
}
