using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputFieldManager : MonoBehaviour
{
    [System.Serializable]
    public class makelist
    {
        public List<InputField> iFields;
    }
    public List<makelist> everyiField = new List<makelist>();

    [System.Serializable]
    public class makelist2
    {
        public List<Text> tObjects;
    }
    public List<makelist2> everytObject = new List<makelist2>();
}
