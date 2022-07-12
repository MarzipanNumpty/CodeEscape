using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    Transform endPos;
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    [SerializeField] float rot;
    bool canMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed);
            if (Vector3.Distance(endPos.position, transform.position) < 1)
            {
                Instantiate(explosion, endPos.position, Quaternion.Euler(rot, 0, 0));
                Destroy(gameObject);
            }
        }
    }

    public void activateMovement(Transform tfrm, Transform explosion, bool isFireball)
    {
        if(isFireball)
        {
            endPos = explosion;
        }
        else
        {
            endPos = tfrm;
        }
        canMove = true;
    }
        
}
