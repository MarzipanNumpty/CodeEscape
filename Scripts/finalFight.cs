using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalFight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "The Adventurer Blake")
        //Object name is the name of the GameObject you want to check for collisions with.
        {
            
            audioManager.instance.PlayMusic(1); //What you want to do on trigger enter
        }
    }
}
