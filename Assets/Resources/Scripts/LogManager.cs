using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : Singleton<LogManager> {

    public int maxLines;

    public int maxFadeLines = 2;
    public int fadeLines;

    private string text;
    private string fadeText;
    private Text logField;
    private Text logFadeField;
    private int lines;

    [SerializeField]
    private float intervalTime = 1.7f;
    private float nextBlankEntryTime = 100;

    //Logs to the log panel
    public void Log(string message)
    {
        lines++;
        if (lines >= maxLines)
        {
            int lineBreakIndex = text.IndexOf("\r\n") + 1;
            LogFadeLine(text.Substring(0, lineBreakIndex));
            text = text.Substring(lineBreakIndex);
        }
        text = text + "\r\n" + message;
        logField.text = text;
        
    }

    private void LogFadeLine(string message) {
        fadeLines++;
        //what the fuck, I just want some god damn lines to scroll through and become transparent
        //WAS THAT TOO MUCH TO ASK FOR
        if (fadeLines >= maxFadeLines)
        {
            int lineBreakIndex = fadeText.IndexOf("\r\n") + 1;
            fadeText = fadeText.Substring(lineBreakIndex);
            fadeLines--;
        }
        fadeText = fadeText + "\r\n" + message;
        logFadeField.text = fadeText;
    }

    private void Start()
    {
        fadeText = "\r\n";
        logField = GameObject.Find("LogValue").GetComponent<Text>();
        logFadeField = GameObject.Find("LogValueTop").GetComponent<Text>();
        for (int i = 0; i < maxLines; i++) {
            Log("");
        }
        nextBlankEntryTime = Time.time + intervalTime;
    }

    void Update() {
        if (Time.time > nextBlankEntryTime) {
            Log("");
            nextBlankEntryTime = Time.time + intervalTime;
        }
    }
}
