using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Terminals/Blank")]
public class Terminal : ScriptableObject
{
    [TextArea]
    public string terminalCode;
    public string terminalName;
    [TextArea]
    public List<string> answers;
    public int linesOfCodeNeeded;
    public int lineOfCodeCompleted;
    public int terminalNum;
}
