using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : Singleton<LogManager>
{
    public int maxLines;

    private string text;
    private Text logField;
    private int lines;

    //Logs to the log panel
    public void Log(string message)
    {
        lines++;
        if (lines >= maxLines)
        {
            int lineBreakIndex = text.IndexOf("\r\n") + 1;
            text = text.Substring(lineBreakIndex);
        }
        text = text + "\r\n" + message;
        logField.text = text;

    }
    public void ClearLog()
    {
        logField.text = "";
        text = "";
    }
    private void Start()
    {
        logField = GameObject.Find("LogValue").GetComponent<Text>();
    }
}