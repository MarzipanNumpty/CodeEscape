using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blueDragonScript : MonoBehaviour
{
    [SerializeField]
    Transform[] flyInPos;
    Transform nextPos;
    public bool startFlying;
    public bool atFinalPos;
    public bool atNextPos;
    public int arrayPos;
    public float speed;
    float startTime;
    float journeyLength;
    float health = 4.0f;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    GameObject dragonCanvas;
    [SerializeField]
    GameObject Player;
    bool lookingAtPlayer;
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        dragonCanvas.SetActive(false);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (startFlying && !atNextPos && !atFinalPos)
        {
            float dist = Vector3.Distance(nextPos.position, transform.position);
            if (dist < 10)
            {
                atNextPos = true;
                getNextPos();
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed);     

            Vector3 targetDir = nextPos.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.04f, 0.0f);

            transform.localRotation = Quaternion.LookRotation(newDir);
        }
        if(atFinalPos && !lookingAtPlayer)
        {
            Vector3 targetDir = nextPos.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.04f, 0.0f);
            float rotationDistance = Mathf.Abs(newDir.x) + Mathf.Abs(newDir.y) + Mathf.Abs(newDir.z);
            if (rotationDistance < 0.5f)
            {
                lookingAtPlayer = true;
            }

            transform.localRotation = Quaternion.LookRotation(newDir);
        }
    }

    public void teleportToFinalPos()
    {
        startFlying = false;
        transform.position = flyInPos[flyInPos.Length - 1].position;
        atFinalPos = true;
        nextPos = Player.transform;
    }

    public void startDragonFlying()
    {
        nextPos = flyInPos[arrayPos];
        
        startFlying = true;
        dragonCanvas.SetActive(true);
    }

    void getNextPos()
    {
        arrayPos++;
        if(arrayPos >= flyInPos.Length)
        {
            nextPos = Player.transform;
            atFinalPos = true;
        }
        else
        {
            nextPos = flyInPos[arrayPos];
            atNextPos = false;
        }
    }

    public void removeHealth(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if(health <= 0)
        {
            healthSlider.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
