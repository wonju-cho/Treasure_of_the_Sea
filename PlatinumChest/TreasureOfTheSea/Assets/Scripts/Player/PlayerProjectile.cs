using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject impactEffect;
    public float radius = 1;
    public int damageAmount = 15;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);

        if(collision.collider.tag == "Enemy")
        {
            Debug.Log("collision detected: enemy");

        }
        else
        {
            Debug.Log("collision detected: wall or something");
        }

        Destroy(gameObject);
    }
}
