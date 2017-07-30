using UnityEngine;
using System.Collections;

public class PaperDoll : MonoBehaviour {

    void Start() {
        int clothingId = Accessorize("clothing", ResourceLoader.instance.girlHairSprites);
        int hairId = Accessorize("hair", ResourceLoader.instance.girlDressSprites);

        PortraitDoll portraitDoll = Object.FindObjectOfType<PortraitDoll>();
        portraitDoll.clothingId = clothingId;
        portraitDoll.hairId = hairId; 
    }

    int Accessorize(string piece, Sprite[] options) {
        SpriteRenderer renderer = transform.Find(piece).GetComponent<SpriteRenderer>();
        int index = Random.Range(0, options.Length);

        renderer.sprite = options[index];

        return index;
    }

}
