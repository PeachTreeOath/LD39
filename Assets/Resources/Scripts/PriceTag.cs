using UnityEngine;
using System.Collections;

public class PriceTag : MonoBehaviour {

    [SerializeField]
    private int cost = 100;
    
    public int amount {
        get { return cost; }
        set {
            cost = value;
            TextMesh textMesh = GetComponent<TextMesh>();
            textMesh.text = string.Format("${0}", value);
        }
    }

    public void FixupRenderOrder() {
        // http://answers.unity3d.com/answers/577922/view.html
        Renderer renderer = GetComponent<Renderer>();
        Renderer parentRenderer = GetComponentInParent<Renderer>();

        renderer.sortingLayerID = parentRenderer.sortingLayerID;
    }
}
