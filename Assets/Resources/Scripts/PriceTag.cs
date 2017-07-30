using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PriceTag : MonoBehaviour {

    [SerializeField]
    private int cost = 99;
    
    public int amount {
        get { return cost; }
        set {
            cost = value;
            Text text = GetComponentInChildren<Text>();
            text.text = string.Format("${0}", value);
        }
    }
}
