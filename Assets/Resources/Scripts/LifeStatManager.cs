using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeStatManager : Singleton<LifeStatManager> {

	public bool isMarried;
    public int educationLevel; //cur education tier
    public int books; //num books in this edu level
    public int totalBooks; //lifetime num books
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
    private RectTransform wealthBar;
    private RectTransform bookBar;
    private RectTransform powerBar;

    private float barHeight;
    private float maxBarWidth;  //wealth and edu
    private float powerBarWidth;

    private float maxCashVal = 3000; //arbitrary but effects bar increment
    private int maxBookLevel = 12; //?

    public int startingWealth;

    private int _wealth = 0; // :-(

    public int wealth
    {
        get { return _wealth; }
        set
        {
            if (_wealth != value)
            {
                _wealth = value;

                BuyablePiece[] buyables = Object.FindObjectsOfType<BuyablePiece>();
                foreach (BuyablePiece buyable in buyables)
                {
                    buyable.OnWealthChange();
                }
            }
        }
    }

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
        //wealthBar = GameObject.Find("WealthBar").GetComponent<RectTransform>();
        //barHeight = wealthBar.rect.height;
        //maxBarWidth = wealthBar.rect.width; 
        //wealthBar.sizeDelta = new Vector2(1, barHeight);
        educationLevelField = GameObject.Find("KnowledgeValue").GetComponent<Text>();
        bookBar = GameObject.Find("KnowledgeBar").GetComponent<RectTransform>();
        bookBar.sizeDelta = new Vector2(1, barHeight);
        //booksField = GameObject.Find("BooksValue").GetComponent<Text>();

        powerBar = GameObject.Find("PowerBar").GetComponent<RectTransform>();
        powerBarWidth = powerBar.rect.width;
        powerBar.sizeDelta = new Vector2(powerBarWidth, barHeight);

        ageField = GameObject.Find("AgeValue").GetComponent<Text>();
        maxAgeField = GameObject.Find("MaxAgeValue").GetComponent<Text>();
        generationField = GameObject.Find("GenerationValue").GetComponent<Text>();

        //InitiallizeFields
        maxAge = StatConstants.instance.initialMaxAge;
        age = StatConstants.instance.initialAge;
        _wealth = startingWealth;

        GameObject.Find("StatsPanel DO NOT DISABLE").gameObject.SetActive(false);
    }

    void Update()
    {
        //Update UI
        //Stats panel currently disabled
        
        //relationshipStatusField.text = StatConstants.instance.RelationshipStatusString(isMarried);
        float curWealth = LifeStatManager.instance.wealth;
        wealthField.text = "$" + curWealth;
        //wealthBar.sizeDelta = new Vector2(maxBarWidth * curWealth / maxCashVal, barHeight);
        educationLevelField.text = StatConstants.instance.EducationString(educationLevel);
        bookBar.sizeDelta = new Vector2(maxBarWidth * totalBooks / maxBookLevel, barHeight);

        float remainingAge = 1 + maxAge - age;
        powerBar.sizeDelta = new Vector2(powerBarWidth * remainingAge / maxAge, barHeight);
        //booksField.text = StatConstants.instance.BooksString(books);
        ageField.text = "" + age;
        maxAgeField.text = "" + maxAge;
        generationField.text = "" + PermanentStatManager.instance.generation;
        
    }

    public void addBooks(int amt) {
        books += amt;
        totalBooks += amt;
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
