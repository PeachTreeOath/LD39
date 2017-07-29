using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeStatManager : Singleton<LifeStatManager> {

	public bool isMarried;
    public int educationLevel;
    public int books;
    public int age;
    public int maxAge;

    //UI fields
    private Text relationshipStatusField;
    private Text wealthField;
    private Text educationLevelField;
    private Text booksField;
    private Text ageField;
    private Text maxAgeField;
    private Text generationField;

    public float GetGoldDropRate()
    {
        return StatConstants.instance.initialGoldDropRate * StatConstants.instance.goldDropRateScalar * educationLevel + StatConstants.instance.initialGoldDropRate;
    }

    void Start()
    {
        relationshipStatusField = GameObject.Find("RelationshipStatusValue").GetComponent<Text>();
        wealthField = GameObject.Find("WealthValue").GetComponent<Text>();
        educationLevelField = GameObject.Find("EducationLevelValue").GetComponent<Text>();
        booksField = GameObject.Find("BooksValue").GetComponent<Text>();
        ageField = GameObject.Find("AgeValue").GetComponent<Text>();
        maxAgeField = GameObject.Find("MaxAgeValue").GetComponent<Text>();
        generationField = GameObject.Find("GenerationValue").GetComponent<Text>();

        //InitiallizeFields
        maxAge = StatConstants.instance.initialMaxAge;
        age = StatConstants.instance.initialAge;
    }

    void Update()
    {
        //Update UI
        relationshipStatusField.text = StatConstants.instance.RelationshipStatusString(isMarried);
        wealthField.text = "$" + PermanentStatManager.instance.wealth;
        educationLevelField.text = StatConstants.instance.EducationString(educationLevel);
        booksField.text = StatConstants.instance.BooksString(books);
        ageField.text = "" + age;
        maxAgeField.text = "" + maxAge;
        generationField.text = "" + PermanentStatManager.instance.generation;
        
    }
}
