using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStatManager : MonoBehaviour {

	public bool isMarried;
    public int educationLevel;
    public int books;
    public int age;
    public int maxAge;

    public float GetGoldDropRate()
    {
        return StatConstants.instance.initialGoldDropRate * StatConstants.instance.goldDropRateScalar * educationLevel + StatConstants.instance.initialGoldDropRate;
    }
}
