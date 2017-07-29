using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConstants : Singleton<StatConstants> {

	//Manages starting values and limits for various fields

    public int booksToLevelEducation;
    public int initialMaxAge;
    public int initialAge;
    public float initialGoldDropRate;
    public float goldDropRateScalar; //multiplies by education level to determine increased gold drop rate

    public string EducationString(int educationLevel)
    {
        switch (educationLevel)
        {
            case 0: return "None";
            case 1: return "High School";
            case 2: return "College";
            case 3: return "Masters";
            case 4: return "PHD";
            default: return "???";
        }
    }

    public string RelationshipStatusString(bool isMarried)
    {
        return isMarried ? "Married" : "Single";
    }

}
