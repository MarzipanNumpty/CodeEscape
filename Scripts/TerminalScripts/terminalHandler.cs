using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalHandler : MonoBehaviour
{
    public Terminal currentTerminal;
    [SerializeField]
    List<Terminal> terminals;

    public void changeTerminal(int terminalNum)
    {
        currentTerminal = terminals[terminalNum];
        Debug.Log(currentTerminal.answers[0]);
    }
}
