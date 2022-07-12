using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTerminals : MonoBehaviour
{
    [SerializeField]
    List<Terminal> allTerminals;

    private void Awake()
    {
        for(int i = 0; i < allTerminals.Count; i++)
        {
            allTerminals[i].lineOfCodeCompleted = 0;
        }
    }
}
