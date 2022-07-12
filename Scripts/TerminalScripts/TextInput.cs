using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    [SerializeField]
    List<InputField> iField;
    List<Text> tObject;
    terminalController controller;
    terminalHandler tHandler;
    inputFieldManager ifManager;
    bool[] terminalAlreadyAccessed = { false, false, false, false, false, false, false, false, false, false };

    private void Awake()
    {
        controller = GetComponent<terminalController>();
        tHandler = GetComponent<terminalHandler>();
        ifManager = GetComponent<inputFieldManager>();
        for (int i = 0; i < ifManager.everyiField.Count; i++)
        {
            updateIFields(i);
        }
        updateIFields(0);
    }

    private void Start()
    {
        
    } 

    public void updateIFields(int currentterminal)
    {
        //iField.Clear();
        iField = ifManager.everyiField[currentterminal].iFields;
        tObject = ifManager.everytObject[currentterminal].tObjects;
        if (terminalAlreadyAccessed[currentterminal] != true)
        {
            //iField = ifManager.everyiField[currentterminal].iFields;
            for (int i = 0; i < iField.Count; i++)
            {
                if (i == 0)
                {
                    iField[0].onEndEdit.AddListener(AcceptStringInput0);
                }
                else if (i == 1)
                {
                    iField[1].onEndEdit.AddListener(AcceptStringInput1);
                }
                else if (i == 2)
                {
                    iField[2].onEndEdit.AddListener(AcceptStringInput2);
                }
                
            }
            terminalAlreadyAccessed[currentterminal] = true;
        }
    }
    void AcceptStringInput0(string userInput)
    {
        controller.compareCode(userInput, iField[0], tObject[0], 0);
    }

    void AcceptStringInput1(string userInput)
    {
        Debug.Log(iField[1]);
        Debug.Log(tObject[1]);
        controller.compareCode(userInput, iField[1], tObject[1], 1);
    }

    void AcceptStringInput2(string userInput)
    {
        controller.compareCode(userInput, iField[2], tObject[2], 2);
    }

    void InputComplete()
    {
       
    }
}
