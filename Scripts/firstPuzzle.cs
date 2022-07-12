using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPuzzle : MonoBehaviour
{
    //First puzzle should be simple. The player should be contained within a small room with only one terminal and a whiteboard. Once the puzzle is solved, the room should expand (walls disabled). 
    //The room should be bare.
    [SerializeField]
    GameObject wall1;
    [SerializeField]
    GameObject wall2;
    [SerializeField]
    GameObject wall3;
    [SerializeField]
    GameObject wall4;
    [SerializeField]
    GameObject wall5;

    public int soundToPlay;

    public void victory()
    {
        wall1.SetActive(false);
        wall2.SetActive(false);
        wall3.SetActive(false);
        wall4.SetActive(false);
        wall5.SetActive(false);
        audioManager.instance.playSFX(soundToPlay);
    }
    
    
}
