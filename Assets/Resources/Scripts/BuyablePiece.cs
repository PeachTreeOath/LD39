using UnityEngine;
using System.Collections;

public class BuyablePiece : BoardPiece {

    // Use this for initialization
    void Start() {
        AddPriceTag(100);
    }

    public override int GetContentType() {
        int cost = 100;  //TODO very experimental! so test! BAD!

        int contentType = BoardPiece.PICKUP;
        if (PermanentStatManager.instance.wealth >= cost) {
            contentType |= BoardPiece.BARRIER;
        }

        return contentType;
    }

    void AddPriceTag(int amount) {
        GameObject instance = Instantiate(ResourceLoader.instance.priceTagFab);
        PriceTag priceTag = instance.GetComponent<PriceTag>();

        instance.transform.SetParent(transform);
        instance.transform.localPosition = Vector2.zero;
        priceTag.FixupRenderOrder();
        priceTag.amount = amount;
    }
}
