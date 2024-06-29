using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestScripText_UI : MonoBehaviour
{
    public TextMeshProUGUI currentTextInteger;
    public int currentInteger;

    public TextMeshProUGUI resultTextInteger;
    public int resultInteger;

    public string questName;
    public Image redLine;

    private void Awake()
    {
        redLine.enabled = false;
        currentInteger = 0;
        currentTextInteger.text = currentInteger.ToString();
    }

    public void SetCurrentQuestText(int integer)
    {
        currentInteger = integer;
        currentTextInteger.text = currentInteger.ToString();
    }

    public void SetResultQuestText(int integer)
    {
        resultInteger = integer;
        resultTextInteger.text = (resultInteger.ToString());
    }

    public int GetCurrentQuestText() { return currentInteger; }

    public int GetResultQuestText() { return resultInteger; }

    public void SetRedLineActive(bool active) { redLine.enabled = active; }

}