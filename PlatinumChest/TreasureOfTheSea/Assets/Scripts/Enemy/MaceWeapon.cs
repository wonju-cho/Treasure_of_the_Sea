using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceWeapon : MonoBehaviour
{
    public GameObject ownerObject;
    public int damageAmount = 20;
    BoxCollider bx;

    private void Start()
    {
        bx = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.tag == "Player")
        {
            if(ownerObject.GetComponent<Animator>().GetBool("isAttacking") == true)
            {
                Debug.Log("Take damage");
                bx.isTrigger = true;
                other.GetComponent<PlayerManager>().TakeDamge(damageAmount);
            }
        }
    }
}
