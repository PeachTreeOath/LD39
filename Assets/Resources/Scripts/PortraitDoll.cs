using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitDoll : MonoBehaviour {

    public int clothing = 1;
    public int hair = 1;

    private SpriteRenderer clothingRenderer;
    private SpriteRenderer hairRenderer;

    public int clothingId {
        get { return clothing;  }
        set {
            if(clothing != value) {
                clothing = value;
                UpdateClothing();
            }
        }
    }

    public int hairId {
        get { return hair; }
        set {
            if(hair != value) {
                hair = value;
                UpdateHair();
            }
        }
    }

	void Start () {
        clothingRenderer = transform.Find("clothing").GetComponent<SpriteRenderer>();
        hairRenderer = transform.Find("hair").GetComponent<SpriteRenderer>();

        UpdateClothing();
        UpdateHair();
	}

    void UpdateClothing() {
        clothingRenderer.sprite = ResourceLoader.instance.portraitGirlDressSprites[clothing];
    }

    void UpdateHair() {
        hairRenderer.sprite = ResourceLoader.instance.portraitGirlHairSprites[hair];
    }
}
