using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class terminalController : MonoBehaviour
{
    public Text displayText;
    terminalHandler tHandler;
    bool t1 = true;
    [SerializeField]
    GameObject[] terminalCanvas;
    TextInput tInput;
    public int previousTerminalNum;
    public int currentTerminalNum;
    terminalWinConditions tWinConds;
    [SerializeField]
    Light[] terminalLights;
    public Color greenColor;
    public Color redColor;

    public List<Action> terminal1 = new List<Action>();
    public List<Action> terminal2 = new List<Action>();
    public List<Action> terminal3 = new List<Action>();
    public List<Action> terminal4 = new List<Action>();
    public List<Action> terminal5 = new List<Action>();
    public List<Action> terminal6 = new List<Action>();
    public List<List<Action>> allTerminals = new List<List<Action>>();
    void Awake()
    {
        tHandler = GetComponent<terminalHandler>();
        tInput = GetComponent<TextInput>();
        tWinConds = GetComponent<terminalWinConditions>();
    }

    private void Start()
    {
        //boii = terminal1Answer1;
        DisplayCode();
        DisplayLoggedText();
        //functionszzz[0] = terminal1Answer1;
        for(int i = 0; i < terminalLights.Length; i ++)
        {
            terminalLights[i].GetComponent<Light>().color = redColor;
        }
        terminal1.Add(terminal1Answer1);
        terminal2.Add(terminal2Answer1);
        terminal3.Add(terminal3Answer1);
        terminal3.Add(terminal3Answer2);
        terminal4.Add(terminal4MultiFunction);
        terminal4.Add(terminal4MultiFunction);
        terminal5.Add(terminal5Answer1);
        terminal6.Add(terminal6MultiFunction);
        terminal6.Add(terminal6MultiFunction);
        allTerminals.Add(terminal1);
        allTerminals.Add(terminal2);
        allTerminals.Add(terminal3);
        allTerminals.Add(terminal4);
        allTerminals.Add(terminal5);
        allTerminals.Add(terminal6);
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", tHandler.currentTerminal.terminalCode);

        displayText.text = logAsText;
    }

    public void DisplayCode()
    {
        //string combinedText = tHandler.currentTerminal.terminalCode + "\n";

        //LogStringWithReturn(combinedText);
    }

    /*public void LogStringWithReturn(string stringToAdd)
    {
        playerCodeInput.Add(stringToAdd + "\n");
    }*/

    public void compareCode(string userCode, InputField tInput, Text tObject, int i)
    {
        Debug.Log(tHandler.currentTerminal.answers[i]);
        List<Action> currentTerminalFunctions = allTerminals[tHandler.currentTerminal.terminalNum];
        List<string> diff;
        List<string> set1 = userCode.Split(' ').ToList();
        List<string> set2 = tHandler.currentTerminal.answers[i].Split(' ').ToList();
        if (set2.Count() > set1.Count())
        {
            diff = set2.Except(set1).ToList();
        }
        else
        {
            diff = set1.Except(set2).ToList();
        }
        int listLength = set1.Count() > set2.Count() ? set2.Count() : set1.Count();
        string newtextOutput = "";
        for(int a = 0; a < listLength; a++)
        {
            if(set1[a] != set2[a])
            {
                set1[a] = "<color=red>" + set1[a]+"</color>";
                Debug.Log(set1[a]);
            }
            else
            {
                set1[a] = "<color=green>" + set1[a] + "</color>";
                //allFunctions[tHandler.currentTerminal.terminalNum].functions[i]();
            }
            newtextOutput += set1[a] + " ";
        }
        Debug.Log(newtextOutput);

        tObject.text = newtextOutput;
        if (userCode == tHandler.currentTerminal.answers[i])
        {
            tInput.interactable = false;
            Debug.Log(tInput);
            tHandler.currentTerminal.lineOfCodeCompleted++;
            currentTerminalFunctions[i]();
        }

        if(tHandler.currentTerminal.lineOfCodeCompleted == tHandler.currentTerminal.linesOfCodeNeeded)
        {
            terminalLights[tHandler.currentTerminal.terminalNum].GetComponent<Light>().color = greenColor;
        }
    }
    
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            changeTerminal();
        }*/
    }

    public void changeTerminal()
    {
        /*if(t1)
        {
            t1 = false;
            terminalCanvas[0].SetActive(false);
            terminalCanvas[1].SetActive(true);
            displayText = terminalCanvas[1].GetComponentInChildren<Text>();
            tInput.updateIFields(1);
            tHandler.changeTerminal(1);
        }
        else
        {
            t1 = true;
            terminalCanvas[0].SetActive(true);
            terminalCanvas[1].SetActive(false);
            displayText = terminalCanvas[0].GetComponentInChildren<Text>();
            tInput.updateIFields(0);
            tHandler.changeTerminal(0);
        }*/
        terminalCanvas[previousTerminalNum].SetActive(false);
        terminalCanvas[currentTerminalNum].SetActive(true);
        displayText = terminalCanvas[currentTerminalNum].GetComponentInChildren<Text>();
        tInput.updateIFields(currentTerminalNum);
        tHandler.changeTerminal(currentTerminalNum);
        DisplayCode();
        DisplayLoggedText();
    }

    public void closeTerminal()
    {
        terminalCanvas[currentTerminalNum].SetActive(false);
    }

    public void terminal1Answer1()
    {
        tWinConds.Terminal1Answer1();
    }

    void terminal2Answer1()
    {
        tWinConds.Terminal2Answer1();
    }

    void terminal3Answer1()
    {
        tWinConds.Terminal3Answer1();
    }
    void terminal3Answer2()
    {
        tWinConds.Terminal3Answer2();
    }
    void terminal4Answer1()
    {
        tWinConds.Terminal4Answer1();
    }
    void terminal5Answer1()
    {
        tWinConds.Terminal5Answer1();
    }
    void terminal6Answer1()
    {
        tWinConds.Terminal6Answer1();
    }

    void terminal4MultiFunction()
    {
        if (tHandler.currentTerminal.lineOfCodeCompleted == tHandler.currentTerminal.linesOfCodeNeeded)
        {
            terminal4Answer1();
        }
    }

    void terminal6MultiFunction()
    {
        if(tHandler.currentTerminal.lineOfCodeCompleted == tHandler.currentTerminal.linesOfCodeNeeded)
        {
            terminal6Answer1();
        }
    }
}
