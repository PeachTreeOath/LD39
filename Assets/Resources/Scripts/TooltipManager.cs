using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : Singleton<TooltipManager> {

    public Dictionary<string, string> wealthTooltips = new Dictionary<string, string>();
    public Dictionary<string, string> healthTooltips = new Dictionary<string, string>();

    void Start()
    {
        //Populate dictionaries here?
    }
}

