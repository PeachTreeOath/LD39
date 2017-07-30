using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : Singleton<TooltipManager>
{

    public Dictionary<string, Tooltip> wealthTooltips = new Dictionary<string, Tooltip>();
    public Dictionary<string, Tooltip> knowledgeTooltips = new Dictionary<string, Tooltip>();
    public Dictionary<string, Tooltip> powerTooltips = new Dictionary<string, Tooltip>();

    void Start()
    {
        knowledgeTooltips["JUNIOR_HIGH"] = new Tooltip("Junior High", 3);
        knowledgeTooltips["HIGH_SCHOOL"] = new Tooltip("High School", 5);
        knowledgeTooltips["COLLEGE"] = new Tooltip("College", 3);
        // Example: int retrieveRequirement = knowledgeTooltips["JUNIOR_HIGH"].requirement;
    }
}

public struct Tooltip
{
     public string name;
     public int requirement;

    public Tooltip(string newName, int newReq)
    {
        name = newName;
        requirement = newReq;
    }
}

