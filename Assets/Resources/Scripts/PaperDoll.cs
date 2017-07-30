using UnityEngine;
using System.Collections;

public class PaperDoll : MonoBehaviour {

    void Start() {
        Accessorize("clothing", ResourceLoader.instance.girlHairSprites);
        Accessorize("hair", ResourceLoader.instance.girlDressSprites);
    }

    void Accessorize(string piece, Sprite[] options) {
        SpriteRenderer renderer = transform.Find(piece).GetComponent<SpriteRenderer>();
        int index = Random.Range(0, options.Length);

        renderer.sprite = options[index];
    }

}
