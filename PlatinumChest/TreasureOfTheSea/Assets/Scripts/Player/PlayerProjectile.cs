using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //public GameObject impactEffect;
    public int damageAmount = 15;
    Rigidbody rb;
    BoxCollider bx;
    bool disableRotation = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
    }

    private void Update()
    {

        //Debug.Log("Hit direction" + rb.velocity);
        if (!disableRotation)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        Destroy(this.gameObject, 5f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("detect collision: " + collision.collider.name);

        if(collision.collider.tag != "Player")
        {
            disableRotation = true;
            rb.isKinematic = true;
            bx.isTrigger = true;
        }

        //GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);
        if (collision.collider.tag == "MeleeEnemy")
        {
            collision.collider.GetComponent<EnemyManage>().TakeDamage(20); 
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "RangeEnemy")
        {
            collision.collider.GetComponent<RangeEnemyAIManage>().TakeDamage(30);
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "Plane")
        {
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "InteractableObject")
        {
            collision.collider.GetComponent<InteractObject>().TakeDamage();
            Destroy(this.gameObject);
        }

    }
}
