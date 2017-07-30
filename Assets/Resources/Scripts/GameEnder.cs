using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : TurnBehaviour {

    public override void OnAdvanceTurn()
    {
        if(LifeStatManager.instance.isMarried)
        {
            PermanentStatManager.instance.generation++;
            PermanentStatManager.instance.currentLevel++;
            SceneManager.LoadScene("Game");
        }
        else if (LifeStatManager.instance.age > LifeStatManager.instance.maxAge)
        {
            // TODO: Replay level instead
            PermanentStatManager.instance.generation++;
            PermanentStatManager.instance.currentLevel++;
            SceneManager.LoadScene("Game");
        }
}

}
