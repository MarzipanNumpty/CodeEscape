using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalWinConditions : MonoBehaviour
{
    //puzzle 1
    [SerializeField]
    GameObject walls;
    //puzzle 2
    [SerializeField]
    GameObject furniture;
    //puzzle 3
    [SerializeField]
    GameObject windowWall;
    [SerializeField]
    GameObject secretFloor;
    //Puzzle 4
    [SerializeField]
    GameObject boxes;
    //puzzle 5
    [SerializeField]
    GameObject myLight;
    //Puzzle 6
    [SerializeField]
    GameObject doorWall;
    [SerializeField]
    GameObject noMaterials;
    [SerializeField]
    GameObject allMaterials;
    [SerializeField]
    GameObject sunLight;



    public void Terminal1Answer1()
    {
        walls.SetActive(false);
    }

    public void Terminal2Answer1()
    {
        furniture.SetActive(true);
    }

    public void Terminal3Answer1()
    {
        windowWall.SetActive(false);
        sunLight.SetActive(true);
    }

    public void Terminal3Answer2()
    {
        secretFloor.SetActive(false);
    }

    public void Terminal4Answer1()
    {
        boxes.SetActive(false);
    }

    public void Terminal5Answer1()
    {
        myLight.SetActive(true);
        noMaterials.SetActive(true);
    }

    public void Terminal6Answer1()
    {
        noMaterials.SetActive(false);
        allMaterials.SetActive(true);
        doorWall.SetActive(false);
    }
}
