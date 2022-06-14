using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
    }


    private void OnCollisionEnter(Collision collision)
    {
        //add audio effect
        //FindObjectOfType<AudioManager>().Play("");
        //GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);

        if(collision.collider.tag != "RangeEnemy")
        {
            Debug.Log("detect with" + collision.collider.name);
            disableRotation = true;
            rb.isKinematic = true;
            bx.isTrigger = true;
        }

        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerManager>().TakeDamge(damageAmount);
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "Plane")
        {
            Destroy(this.gameObject);
        }
        //Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //foreach(Collider nearbyObject in colliders)
        //{
            
        //    if (nearbyObject.tag == "Player")
        //    {
        //        //playermanager.takedamage();
        //    }
        //}
    }
}
