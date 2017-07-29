using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : TurnBehaviour {

    public override void OnAdvanceTurn()
    {
        if (LifeStatManager.instance.age > LifeStatManager.instance.maxAge)
        {
            PermanentStatManager.instance.generation++;
            SceneManager.LoadScene("Game");
        }
    }

}
