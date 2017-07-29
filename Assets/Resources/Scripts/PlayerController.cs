using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AdvanceTurn() {
        TurnBehaviour[] turnBehaviours = Object.FindObjectsOfType<TurnBehaviour>();
        foreach(TurnBehaviour behaviour in turnBehaviours) {
            behaviour.OnAdvanceTurn();
        }
    }
}
