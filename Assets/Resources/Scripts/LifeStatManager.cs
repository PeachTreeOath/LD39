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
        return 5; // selected by a random die roll
        //return StatConstants.instance.initialGoldDropRate * StatConstants.instance.goldDropRateScalar * educationLevel + StatConstants.instance.initialGoldDropRate;
    }

    void Start()
    {
        //Where is the relationship bar??
        //relationshipStatusField = GameObject.Find("RelationshipStatusValue").GetComponent<Text>();
        wealthField = GameObject.Find("CashValue").GetComponent<Text>();
        educationLevelField = GameObject.Find("KnowledgeValue").GetComponent<Text>();
        //booksField = GameObject.Find("BooksValue").GetComponent<Text>();
        ageField = GameObject.Find("AgeValue").GetComponent<Text>();
        maxAgeField = GameObject.Find("MaxAgeValue").GetComponent<Text>();
        generationField = GameObject.Find("GenerationValue").GetComponent<Text>();

        //InitiallizeFields
        maxAge = StatConstants.instance.initialMaxAge;
        age = StatConstants.instance.initialAge;

        GameObject.Find("StatsPanel").gameObject.SetActive(false);
    }

    void Update()
    {
        //Update UI
        //Stats panel currently disabled
        
        //relationshipStatusField.text = StatConstants.instance.RelationshipStatusString(isMarried);
        wealthField.text = "$" + PermanentStatManager.instance.wealth;
        educationLevelField.text = StatConstants.instance.EducationString(educationLevel);
        //booksField.text = StatConstants.instance.BooksString(books);
        ageField.text = "" + age;
        maxAgeField.text = "" + maxAge;
        generationField.text = "" + PermanentStatManager.instance.generation;
        
    }

    public void addBooks(int amt) {
        books += amt;
        int max = StatConstants.instance.booksToLevelEducation;
        while (books >= max) {
            books -= max;
            addEducation(1);
        }
    }

    public void addEducation(int amt) {
        int oldEducationLevel = educationLevel;
        int newEducationLevel = educationLevel + amt;

        if(newEducationLevel > oldEducationLevel) {
            for(int keyId = oldEducationLevel; keyId < newEducationLevel; keyId++) {
                UnlockZone(keyId);
            }
        }

        educationLevel = newEducationLevel;        
    }

    protected void UnlockZone(int keyId) {
        foreach (Board bb in BoardManager.instance.getAllBoards()) {
            bb.useKey(keyId);
        }
    }
}
