using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyablePiece : BoardPiece {

    private int cost = 0;
    private bool canBuy = false;
    private bool pickedUp = false;

    private PriceTag priceTag = null;

    public int price {
        get { return cost; }
        set {
            int oldCost = cost;
            cost = value;
            if(oldCost != cost) {
                if (cost > 0) {
                    if (priceTag == null) AddPriceTag();
                    priceTag.amount = cost;
                }

                UpdateCanBuy();
            }
        }
    }

    public override int GetContentType() {
        int contentType = BoardPiece.PICKUP;
        if(canBuy) contentType |= BoardPiece.BARRIER;

        return contentType;
    }

    void AddPriceTag() {
        GameObject instance = Instantiate(ResourceLoader.instance.priceTagFab);
        instance.transform.SetParent(transform);
        instance.transform.localPosition = Vector2.zero;
        
        priceTag = instance.GetComponent<PriceTag>();
        priceTag.GetComponentInChildren<Text>().rectTransform.localPosition = new Vector2(0, 0.25f);
    }

    public override void OnPickup() {
        pickedUp = true;
        PermanentStatManager.instance.wealth -= price;
    }

    public virtual void OnWealthChange() {
        if(!pickedUp) {
            UpdateCanBuy();
        }
    }

    public override void OnRemove() {
        board.UpdatePiece_Remove(this, x, y); 
        Destroy(gameObject);
    }

    private void UpdateCanBuy() {
        if(board != null) {
            board.UpdatePiece_Remove(this, x, y);
        }

        canBuy = PermanentStatManager.instance.wealth < price;

        if(board != null) {
            board.UpdatePiece_Place(this, x, y);
        }
    }
}
